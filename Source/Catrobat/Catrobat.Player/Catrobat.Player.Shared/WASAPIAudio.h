#pragma once

#include <windows.h>
#include <synchapi.h>
#include <audioclient.h>
#include <phoneaudioclient.h>

namespace WasapiComp
{
	ref class WASAPIAudio sealed
	{
	public:

		WASAPIAudio();
		virtual ~WASAPIAudio();

		bool StartAudioCapture();
		bool StopAudioCapture();
		int ReadBytes(Platform::Array<byte>^* a);

	private:

		HRESULT InitCapture();

		bool started;
		int m_sourceFrameSizeInBytes;

		WAVEFORMATEX* m_waveFormatEx;

		IAudioClient2* m_pDefaultCaptureDevice;
		IAudioCaptureClient* m_pCaptureClient;

	};
}
