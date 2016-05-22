#include <fileapi.h>
#include <windows.h>

#include "pch.h"
#include "Constants.h"
#include "RiffReader.h"

std::unique_ptr<XAUDIO2_BUFFER> RiffReader::Read(HANDLE fileHandle, WAVEFORMATEX* wfx)
{
	// Search for RIFF chunk
	std::unique_ptr<Chunk> riffChunk = FindChunk(fileHandle, Constants::Sound::fourccRIFF);
	if(!riffChunk)
	{
		return NULL;
	}
	DWORD filetype;
	HRESULT hr = ReadChunkData(fileHandle, &filetype, sizeof(DWORD), riffChunk->data);

	// Check for correct file type (WAVE)
	if(hr != S_OK || filetype != Constants::Sound::fourccWAVE)
	{
		return NULL;
	}

	// Finde format and data chunks
	std::unique_ptr<Chunk> formatChunk = FindChunk(fileHandle, Constants::Sound::fourccFMT);
	std::unique_ptr<Chunk> dataChunk = FindChunk(fileHandle, Constants::Sound::fourccDATA);
	if(!formatChunk || !dataChunk)
	{
		return NULL;
	}

	// Read format and data in buffers
	BYTE* dataBuffer = new BYTE[dataChunk->size]; // No shared pointer, since XAUDIO2_BUFFER will need this raw pointer anyway
	hr = ReadChunkData(fileHandle, dataBuffer, dataChunk->size, dataChunk->data);
	if(hr != S_OK)
	{
		return NULL;
	}
	hr = ReadChunkData(fileHandle, wfx, formatChunk->size, formatChunk->data);
	if(hr != S_OK)
	{
		return NULL;
	}

	// Create XAUDIO2_BUFFER object
	std::unique_ptr<XAUDIO2_BUFFER> xAudioBuffer = std::make_unique<XAUDIO2_BUFFER>();
	xAudioBuffer->Flags = XAUDIO2_END_OF_STREAM;
	xAudioBuffer->AudioBytes = dataChunk->size;
	xAudioBuffer->pAudioData = dataBuffer;
	return xAudioBuffer;
}

std::unique_ptr<Chunk> RiffReader::FindChunk(HANDLE fileHandle, DWORD fourcc)
{
	// LARGE_INTEGER needed for SetFilePointerEx (correct usage?)
	LARGE_INTEGER largeInt;
	largeInt.LowPart = 0;
	largeInt.HighPart = 0;
	
	// Set file pointer to beginning of file
	if(INVALID_SET_FILE_POINTER == SetFilePointerEx(fileHandle, largeInt, NULL, FILE_BEGIN))
	{
		return NULL;
	}
	DWORD dwChunkType;
	DWORD dwChunkDataSize;
	DWORD dwRIFFDataSize = 0;
	DWORD dwFileType;
	DWORD dwOffset = 0;
	while(true)
	{
		// Read chunk type and chunk data size from file handle
		DWORD dwRead;
		HRESULT hr = 0;
		if (0 == ReadFile(fileHandle, &dwChunkType, sizeof(DWORD), &dwRead, NULL) || dwRead == 0)
		{
			return NULL;
		}

		if(0 == ReadFile(fileHandle, &dwChunkDataSize, sizeof(DWORD), &dwRead, NULL))
		{
			return NULL;
		}

		switch(dwChunkType)
		{
			// If chunk type is RIFF, structure also contains file type additionally
			case Constants::Sound::fourccRIFF:
				dwRIFFDataSize = dwChunkDataSize;
				dwChunkDataSize = 4;
				if(0 == ReadFile(fileHandle, &dwFileType, sizeof(DWORD), &dwRead, NULL))
				{
					return NULL;
				}
				break;

			// Move pointer to next chunk
			default:
				LARGE_INTEGER largeChunkDataSize;
				largeChunkDataSize.LowPart = dwChunkDataSize;
				largeChunkDataSize.HighPart = 0;
				if(INVALID_SET_FILE_POINTER == SetFilePointerEx(fileHandle, largeChunkDataSize, NULL, FILE_CURRENT))
				{
					return NULL;
				}
		}

		// Update current offset (data type and size read)
		dwOffset += sizeof(DWORD) * 2;

		// Have we found the searched chunk?
		if(dwChunkType == fourcc)
		{
			std::unique_ptr<Chunk> info = std::make_unique<Chunk>();
			info->size = dwChunkDataSize;
			info->data = dwOffset;
			return info;
		}

		// Update offset now with chunk data itself
		dwOffset += dwChunkDataSize;
	}
	return NULL;
}

HRESULT RiffReader::ReadChunkData(HANDLE fileHandle, void *	buffer, DWORD bufferSize, DWORD bufferOffset)
{

	if (!buffer)
	{
		return S_FALSE;
	}

	// Read the data in fileHandle into buffer
	HRESULT hr = S_OK;

	// LARGE_INTEGER needed for SetFilePointerEx (correct usage?)
	LARGE_INTEGER largeBufferOffset;
	largeBufferOffset.LowPart = bufferOffset;
	largeBufferOffset.HighPart = 0;

	// Set file pointer to correct offset
	if(INVALID_SET_FILE_POINTER == SetFilePointerEx(fileHandle, largeBufferOffset, NULL, FILE_BEGIN))
		return HRESULT_FROM_WIN32(GetLastError());

	// Read the data
	DWORD dwRead;
	if(0 == ReadFile(fileHandle, buffer, bufferSize, &dwRead, NULL))
		hr = HRESULT_FROM_WIN32(GetLastError());
	return hr;
}