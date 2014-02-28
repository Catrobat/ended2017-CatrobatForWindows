#pragma once

class MicrophoneProvider
{
public:
	MicrophoneProvider();
	~MicrophoneProvider();

	bool startRecording();
	bool stopRecording();

private:
	//IAsyncOperation<AudioVideoCaptureDevice^>^ m_audio_device;
	Windows::Phone::Media::Capture::AudioVideoCaptureDevice^ m_audio_device;
	Windows::Storage::Streams::IRandomAccessStream^ m_stream;
	bool Init();


};