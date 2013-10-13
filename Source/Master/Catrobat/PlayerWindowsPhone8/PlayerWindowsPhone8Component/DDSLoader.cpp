#include "pch.h"
#include "DDSLoader.h"
#include "lodepng.h"
#include "DDSTextureLoader.h"
#include "PlayerException.h"
#include "Helper.h"

#include <D3D11.h>

using namespace Microsoft::WRL;
using namespace Windows::Storage;
using namespace Windows::Storage::FileProperties;
using namespace Windows::Storage::Streams;
using namespace Windows::Foundation;
using namespace Windows::ApplicationModel;
using namespace concurrency;
using namespace DirectX;
using namespace std;


void DDSLoader::ConvertToDDS(vector<unsigned char> image, unsigned int width, unsigned int height, BYTE **stream, unsigned int *streamSize)
{
	// Set HEADER values ---------------------------------------------------------------------------------------
	DDS_HEADER ddsHeader;
	unsigned int m_streamLength;
	int index = 0;
	BYTE *bdata = new byte[image.size()];
	for (unsigned int index = 0; index < image.size();)
	{
		bdata[index + 2] = image.at(index + 0);     // A
		bdata[index + 1] = image.at(index + 1);     // R
		bdata[index + 0] = image.at(index + 2);		// G
		bdata[index + 3] = image.at(index + 3);		// B
		index += 4;
	}
	m_streamLength = image.size();
	ddsHeader.dwSize = DDSD_HEADERSIZE;
	ddsHeader.dwWidth = width;
	ddsHeader.dwHeight = height;
	ddsHeader.dwFlags = DDSD_CAPS | DDSD_HEIGHT | DDSD_WIDTH | DDSD_PITCH | DDSD_PIXELFORMAT;
	ddsHeader.dwPitchOrLinearSize = (width * 32 /* bits per pixel */ + 7) / 8;
	ddsHeader.ddspf.dwSize = DDPF_HEADERSIZE;
	ddsHeader.ddspf.dwFlags = DDPF_ALPHAPIXELS | DDPF_RGB;
	ddsHeader.ddspf.dwFourCC = 0;
	ddsHeader.ddspf.dwRGBBitCount = 32; // RGBA
	ddsHeader.ddspf.dwABitMask = DDPF_AMASK;
	ddsHeader.ddspf.dwRBitMask = DDPF_RMASK;
	ddsHeader.ddspf.dwGBitMask = DDPF_GMASK;
	ddsHeader.ddspf.dwBBitMask = DDPF_BMASK;
	ddsHeader.dwCaps = DDSCAPS_TEXTURE;

	// Not required
	ddsHeader.dwDepth = 0;
	ddsHeader.dwMipMapCount = 0;
	for (int i = 0; i < 11; i++)
	{
		ddsHeader.dwReserved1[i] = 0;
	}

	ddsHeader.dwCaps2 = 0;
	ddsHeader.dwCaps3 = 0;
	ddsHeader.dwCaps4 = 0;
	ddsHeader.dwReserved2 = 0;

	// Prepare STREAM for WRITING
	*streamSize = 4 + ddsHeader.dwSize + m_streamLength;
	*stream = new BYTE[*streamSize];

	// WRITE STREAM --------------------------------------------------------------------------------------------
	int byteIndex = 0;

	// WRITE DWORD dwMagic
	WriteDWord(*stream, &byteIndex, DDSD_MAGICNUMBER);

	// Write DDS_HEADER
	WriteDWord(*stream, &byteIndex, ddsHeader.dwSize);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwFlags);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwHeight);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwWidth);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwPitchOrLinearSize);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwDepth);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwMipMapCount);
	for (unsigned int i = 0; i < 11; i++)
	{
		WriteDWord(*stream, &byteIndex, ddsHeader.dwReserved1[i]);
	}

	// Write DDS_PIXELFORMAT 
	WriteDWord(*stream, &byteIndex, ddsHeader.ddspf.dwSize);
	WriteDWord(*stream, &byteIndex, ddsHeader.ddspf.dwFlags);
	WriteDWord(*stream, &byteIndex, ddsHeader.ddspf.dwFourCC);
	WriteDWord(*stream, &byteIndex, ddsHeader.ddspf.dwRGBBitCount);
	WriteDWord(*stream, &byteIndex, ddsHeader.ddspf.dwRBitMask);
	WriteDWord(*stream, &byteIndex, ddsHeader.ddspf.dwGBitMask);
	WriteDWord(*stream, &byteIndex, ddsHeader.ddspf.dwBBitMask);
	WriteDWord(*stream, &byteIndex, ddsHeader.ddspf.dwABitMask);

	// Write DDS_HEADER 
	WriteDWord(*stream, &byteIndex, ddsHeader.dwCaps);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwCaps2);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwCaps3);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwCaps4);
	WriteDWord(*stream, &byteIndex, ddsHeader.dwReserved2);

	for (unsigned int index = 0; index < m_streamLength; index++)
	{
		((*stream)[byteIndex++]) = bdata[index];
	}
}

void DDSLoader::WriteDWord(BYTE *stream, int *byteIndex, DWORD dword)
{
	BYTE data4 = dword & 0x000000FF;
	dword = dword >> 8;
	BYTE data3 = dword & 0x000000FF;
	dword = dword >> 8;
	BYTE data2 = dword & 0x000000FF;
	dword = dword >> 8;
	BYTE data1 = dword & 0x000000FF;
	stream[(*byteIndex)++] = data4;
	stream[(*byteIndex)++] = data3;
	stream[(*byteIndex)++] = data2;
	stream[(*byteIndex)++] = data1;
}

void DDSLoader::LoadTexture(ID3D11Device* d3dDevice, string filename, ID3D11ShaderResourceView** texture, unsigned int *width, unsigned int *height)
{
	std::vector<unsigned char> image; //the raw pixels
	unsigned error = lodepng::decode(image, *width, *height, filename);

	switch (error)
	{
	case 0:
		{
			BYTE *stream = nullptr;
			unsigned int streamSize;
			ConvertToDDS(image, *width, *height, &stream, &streamSize);
			CreateDDSTextureFromMemory(d3dDevice, stream, streamSize, nullptr, texture, MAXSIZE_T);
			break;
		}
	case 48:
		{
			throw new PlayerException("LodePNG Error. Texture not found. Invalid Path.");
			break;
		}
	default:
		{
			throw new PlayerException("LodePNG Error. Error code: " + Helper::ConvertPlatformStringToString(error.ToString()));
			break;
		}
		break;
	}
}

