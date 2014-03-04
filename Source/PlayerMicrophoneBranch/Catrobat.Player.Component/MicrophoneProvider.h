#pragma once

using namespace Windows::Phone::Media::Capture;

class MicrophoneProvider
{
public:
	MicrophoneProvider();
	~MicrophoneProvider();

	//bool startRecording();
	//bool stopRecording();

private:/*
	IAsyncOperation<AudioVideoCaptureDevice^>^ m_microphoneDevice;
	AudioVideoCaptureDevice^ m_audio_device;
	Windows::Storage::Streams::IRandomAccessStream^ m_stream;
	bool Init();

	*/
};