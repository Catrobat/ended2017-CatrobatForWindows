#pragma once

#include <ppltasks.h> 
#include <vector>
#include <string>

class DDSLoader
{
public:

	struct DDS_PIXELFORMAT {
	  DWORD dwSize;
	  DWORD dwFlags;
	  DWORD dwFourCC;
	  DWORD dwRGBBitCount;
	  DWORD dwRBitMask;
	  DWORD dwGBitMask;
	  DWORD dwBBitMask;
	  DWORD dwABitMask;
	};

	typedef struct {
	  DWORD           dwSize;
	  DWORD           dwFlags;
	  DWORD           dwHeight;
	  DWORD           dwWidth;
	  DWORD           dwPitchOrLinearSize;
	  DWORD           dwDepth;
	  DWORD           dwMipMapCount;
	  DWORD           dwReserved1[11];
	  DDS_PIXELFORMAT ddspf;
	  DWORD           dwCaps;
	  DWORD           dwCaps2;
	  DWORD           dwCaps3;
	  DWORD           dwCaps4;
	  DWORD           dwReserved2;
	} DDS_HEADER;

	DWORD               dwMagic;	// 'DDS' (0x20534444)
	DDS_HEADER          header;
	BYTE				bdata[100000000];	// We need this for the main surface data
	//BYTE				bdata2[1];	// Other surface data

	DDSLoader(std::vector<unsigned char> image);

	void WriteFile();

private:
	std::vector<unsigned char> m_image;
	Windows::Storage::StorageFolder^ m_location; 
    Platform::String^ m_locationPath; 


};

