#include "pch.h"
#include "DDSLoader.h"

using namespace Microsoft::WRL;
using namespace Windows::Storage;
using namespace Windows::Storage::FileProperties;
using namespace Windows::Storage::Streams;
using namespace Windows::Foundation;
using namespace Windows::ApplicationModel;
using namespace concurrency;
using namespace std;

DDSLoader::DDSLoader(vector<unsigned char> image, unsigned int width,  unsigned int height)
{
	int index = 0;
	bdata = new byte[image.size()];
	for (int index = 0; index < image.size();)
	{
		bdata[index] = image.at(index + 3);     // A
		bdata[index + 1] = image.at(index);     // R
		bdata[index + 2] = image.at(index + 1); // G
		bdata[index + 3] = image.at(index + 2); // B
		index += 4;
	}
	m_streamLength = image.size();
	m_ddsHeader.dwSize = DDSD_HEADERSIZE;
	m_ddsHeader.dwWidth = width;
	m_ddsHeader.dwHeight = height;
	m_ddsHeader.dwFlags = DDSD_CAPS || DDSD_HEIGHT || DDSD_WIDTH || DDSD_PITCH || DDSD_PIXELFORMAT;
	m_ddsHeader.dwPitchOrLinearSize = (width * 32 /* bits per pixel */ + 7) / 8;
	m_ddsHeader.ddspf.dwSize = DDPF_HEADERSIZE;
	m_ddsHeader.ddspf.dwFlags = DDPF_ALPHAPIXELS || DDPF_RGB;
	m_ddsHeader.ddspf.dwRGBBitCount = 32; // RGBA
	m_ddsHeader.ddspf.dwABitMask = DDPF_AMASK;
	m_ddsHeader.ddspf.dwRBitMask = DDPF_RMASK;
	m_ddsHeader.ddspf.dwGBitMask = DDPF_GMASK;
	m_ddsHeader.ddspf.dwBBitMask = DDPF_BMASK;
	m_ddsHeader.dwCaps = DDSCAPS_TEXTURE;

	// Not required
	m_ddsHeader.dwDepth = 0;
	m_ddsHeader.dwMipMapCount = 0;
	for (int i = 0; i < 11; i++)
	{
		m_ddsHeader.dwReserved1[i] = 0;
	}

	m_ddsHeader.dwCaps2 = 0;
	m_ddsHeader.dwCaps3 = 0;
	m_ddsHeader.dwCaps4 = 0;
	m_ddsHeader.dwReserved2 = 0;

	m_location = Package::Current->InstalledLocation; 
	m_locationPath = Platform::String::Concat(m_location->Path, "//"); 
}

void DDSLoader::WriteFile()
{
	PCWSTR SaveStateFile = L"testpic.dds";
    auto folder = ApplicationData::Current->LocalFolder;
    task<StorageFile^> getFileTask(folder->CreateFileAsync(ref new Platform::String(SaveStateFile), CreationCollisionOption::ReplaceExisting));

    auto writer = std::make_shared<Streams::DataWriter^>(nullptr);

    getFileTask.then([](StorageFile^ file)
    {
        return file->OpenAsync(FileAccessMode::ReadWrite);
    }).then([this, writer](Streams::IRandomAccessStream^ stream)
    {
        Streams::DataWriter^ state = ref new Streams::DataWriter(stream);
        *writer = state;
		
		// WRITE DWORD dwMagic
		state->WriteByte(DDSD_MAGICNUMBER);

		// Write DDS_HEADER 
		WriteDWord(state, m_ddsHeader.dwSize);
		WriteDWord(state, m_ddsHeader.dwFlags);
	    WriteDWord(state, m_ddsHeader.dwHeight);
	    WriteDWord(state, m_ddsHeader.dwWidth);
	    WriteDWord(state, m_ddsHeader.dwPitchOrLinearSize);
        WriteDWord(state, m_ddsHeader.dwDepth);
	    WriteDWord(state, m_ddsHeader.dwMipMapCount);
	    WriteDWord(state, m_ddsHeader.dwReserved1[11]);

		// Write DDS_PIXELFORMAT 
		WriteDWord(state, m_ddsHeader.ddspf.dwSize);
		WriteDWord(state, m_ddsHeader.ddspf.dwFlags);
		WriteDWord(state, m_ddsHeader.ddspf.dwFourCC);
		WriteDWord(state, m_ddsHeader.ddspf.dwRGBBitCount);
		WriteDWord(state, m_ddsHeader.ddspf.dwRBitMask);
		WriteDWord(state, m_ddsHeader.ddspf.dwGBitMask);
		WriteDWord(state, m_ddsHeader.ddspf.dwBBitMask);
		WriteDWord(state, m_ddsHeader.ddspf.dwABitMask);

		// Write DDS_HEADER 
	    WriteDWord(state, m_ddsHeader.dwCaps);
	    WriteDWord(state, m_ddsHeader.dwCaps2);
        WriteDWord(state, m_ddsHeader.dwCaps3);
	    WriteDWord(state, m_ddsHeader.dwCaps4);
	    WriteDWord(state, m_ddsHeader.dwReserved2);

		for (int index = 0; index < m_streamLength; index++)
		{
			state->WriteByte(bdata[index]);
		}

        return state->StoreAsync();
    }).then([writer](uint32 count)
    {
        return (*writer)->FlushAsync();
    }).then([this, writer](bool flushed)
    {
        delete (*writer);
    });
}

void DDSLoader::WriteDWord(Streams::DataWriter^ state, DWORD dword)
{
	BYTE data4 = dword & 0x000000FF;
	dword = dword >> 8;
	BYTE data3= dword & 0x000000FF;
	dword = dword >> 8;
	BYTE data2 = dword & 0x000000FF;
	dword = dword >> 8;
	BYTE data1 = dword & 0x000000FF;
	state->WriteByte(data1);
	state->WriteByte(data2);
	state->WriteByte(data3);
	state->WriteByte(data4);
}