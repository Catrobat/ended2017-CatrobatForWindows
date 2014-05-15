#include "pch.h"
#include "LoudnessCapture.h"

#include "DeviceInformation.h"
#include "PlayerException.h"

using namespace WasapiComp;

LoudnessCapture::LoudnessCapture() 
{
	m_wasapiAudio = ref new WASAPIAudio();
	m_isRecording = false;
	m_loudness = 0.0;
}

double LoudnessCapture::GetLoudness()
{
	return m_loudness;
}

bool LoudnessCapture::StartCapture()
{
	if (DeviceInformation::IsRunningOnDevice() != true)
		throw new PlayerException("init WASAPI failed, Microphone is not supported");

	if (!m_isRecording)
	{
		if (m_wasapiAudio->StartAudioCapture())
		{
			m_isRecording = true;
			m_loudness = 1;
			return true;
		}
	}
	
	return false;
}

bool LoudnessCapture::StopCapture()
{
	if (m_isRecording)
	{
		if (m_wasapiAudio->StopAudioCapture())
		{
			m_isRecording = false;
			return true;
		}
	}

	return false;
}

