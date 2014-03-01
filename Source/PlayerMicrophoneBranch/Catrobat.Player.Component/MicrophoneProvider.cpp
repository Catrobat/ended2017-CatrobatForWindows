#include "pch.h"
#include "MicrophoneProvider.h"

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
	return true;
}

bool MicrophoneProvider::Init()
{
	//m_audio_device = AudioVideoCaptureDevice::OpenForAudioOnlyAsync();
	auto success = false;

	if (m_audio_device != nullptr)
	{
		success = true;
	}
	return success;
}
