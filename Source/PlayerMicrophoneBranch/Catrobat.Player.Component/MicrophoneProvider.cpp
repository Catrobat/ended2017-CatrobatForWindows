#include "pch.h"
#include "MicrophoneProvider.h"
#include <ppltasks.h>
using namespace Windows::Foundation;
using namespace Windows::Phone::Media::Capture;
using namespace Windows::Storage::Streams;



MicrophoneProvider::MicrophoneProvider()
{
	Init();
}

MicrophoneProvider::~MicrophoneProvider()
{
}

bool MicrophoneProvider::startRecording()
{
	m_audio_device->StartRecordingToSinkAsync();
	return true;
}

bool MicrophoneProvider::stopRecording()
{
	m_audio_device->StopRecordingAsync();
	//m_audio_device->StartRecordingToStreamAsync(m_stream);
	return true;
}

bool MicrophoneProvider::Init()
{
	m_microphoneDevice = AudioVideoCaptureDevice::OpenForAudioOnlyAsync();
	m_audio_device = dynamic_cast<AudioVideoCaptureDevice^> (AudioVideoCaptureDevice::OpenForAudioOnlyAsync());
	m_audio_device->StartRecordingToStreamAsync(m_stream);

	for (auto i = 0; i < MAXINT32; i++)
	{
		for (auto j = 0; j < MAXINT32; j++)
		{

		}
	}
	m_audio_device->StopRecordingAsync();

	
	//m_stream = new IRandomAccessStream();
	//m_audio_device = AudioVideoCaptureDevice::OpenForAudioOnlyAsync();
	auto success = false;

	//if (m_audio_device != nullptr)
	{
		success = true;
	}
	return success;
}
