#pragma once

#include <xaudio2.h>

#include "pch.h"

using Windows::Storage::Streams::IBuffer;
using std::string;

struct AudioData
{
	DWORD data;
	DWORD numberOfBytes;
	WAVEFORMATEX* waveFormat;
};

struct Chunk
{
	DWORD data;
	DWORD size;
};

class RiffReader
{
private:
public:
	std::unique_ptr<XAUDIO2_BUFFER> Read(HANDLE fileHandle, WAVEFORMATEX* wfx);
	std::unique_ptr<Chunk> FindChunk(HANDLE fileHandle, DWORD fourcc);
	HRESULT ReadChunkData(HANDLE fileHandle, void *buffer, DWORD bufferSize, DWORD bufferOffset);
};