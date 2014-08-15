#pragma once

//#include <wrl/client.h>
//#include <ppl.h>
//#include <ppltasks.h>

namespace DX
{
	inline void ThrowIfFailed(HRESULT hr)
	{
		if (FAILED(hr))
		{
            // Set a breakpoint on this line to catch DX API errors.
			throw Platform::Exception::CreateException(hr);
		}
	}

#if defined(_DEBUG)
    // Check for SDK Layer support.
    inline bool SdkLayersAvailable()
    {
        HRESULT hr = D3D11CreateDevice(
            nullptr,
            D3D_DRIVER_TYPE_NULL,       // There is no need to create a real hardware device.
            0,
            D3D11_CREATE_DEVICE_DEBUG,  // Check for the SDK layers.
            nullptr,                    // Any feature level will do.
            0,
            D3D11_SDK_VERSION,          // Always set this to D3D11_SDK_VERSION for Windows Store apps.
            nullptr,                    // No need to keep the D3D device reference.
            nullptr,                    // No need to know the feature level.
            nullptr                     // No need to keep the D3D device context reference.
            );

        return SUCCEEDED(hr);
    }
#endif

	// Function that reads from a binary file asynchronously.
	//inline Concurrency::task<Platform::Array<byte>^> ReadDataAsync(Platform::String^ filename)
	//{
	//	using namespace Windows::Storage;
	//	using namespace Concurrency;
	//	
	//	auto folder = Windows::ApplicationModel::Package::Current->InstalledLocation;
	//	
	//	return create_task(folder->GetFileAsync(filename)).then([] (StorageFile^ file) 
	//	{
	//		return file->OpenReadAsync();
	//	}).then([] (Streams::IRandomAccessStreamWithContentType^ stream)
	//	{
	//		unsigned int bufferSize = static_cast<unsigned int>(stream->Size);
	//		auto fileBuffer = ref new Streams::Buffer(bufferSize);
	//		return stream->ReadAsync(fileBuffer, bufferSize, Streams::InputStreamOptions::None);
	//	}).then([] (Streams::IBuffer^ fileBuffer) -> Platform::Array<byte>^ 
	//	{
	//		auto fileData = ref new Platform::Array<byte>(fileBuffer->Length);
	//		Streams::DataReader::FromBuffer(fileBuffer)->ReadBytes(fileData);
	//		return fileData;
	//	});
	//}
}