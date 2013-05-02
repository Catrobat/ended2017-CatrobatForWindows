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

DDSLoader::DDSLoader(vector<unsigned char> image)
{
	int index = 0;
	for (vector<unsigned char>::iterator it = m_image.begin(); it != m_image.end(); it++)
	{
		// We have R32G32B32A32, but Microsoft tells me to use A8R8G8B8, A1R5G5B5, 
		// A4R4G4B4, R8G8B8, R5G6B5, so lets see if this will work
		bdata[index++] = *it; 
	}
	m_image = image;
	m_location = Package::Current->InstalledLocation; 
	m_locationPath = Platform::String::Concat(m_location->Path, "//"); 
}

void DDSLoader::WriteFile()
{
	PCWSTR SaveStateFile = L"testasave.txt";
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
		
		for (vector<unsigned char>::iterator it = m_image.begin(); it != m_image.end(); it++)
		{
			state->WriteByte(*it);
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