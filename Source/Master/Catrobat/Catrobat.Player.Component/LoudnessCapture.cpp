#include "pch.h"
#include "LoudnessCapture.h"

#include "DeviceInformation.h"
#include "PlayerException.h"

using namespace WasapiComp;
using namespace Windows::System;
using namespace Windows::System::Threading;
using namespace Windows::Foundation;
using namespace Platform;

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
			m_loudness = 0;

			StartPeriodicCalculationLoudness();
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

			m_timer->Cancel();
			return true;
		}
	}

	return false;
}

void LoudnessCapture::StartPeriodicCalculationLoudness()
{
	//Thread Dummy
	/*
	std::shared_ptr<int> sharedVal = std::make_shared<int>(0);
	*sharedVal = 0;

	TimeSpan delay;
	delay.Duration = 10000000;

	m_timer = ThreadPoolTimer::CreatePeriodicTimer(
		ref new TimerElapsedHandler([this, sharedVal](ThreadPoolTimer^ source)
	{
		if (*sharedVal == 59)
		{
			*sharedVal = 0;
			this->UpdateLoudness(*sharedVal);
		}
		else
		{
			*sharedVal += 1;
			this->UpdateLoudness(*sharedVal);
		}
	}), delay, 
		ref new TimerDestroyedHandler([&](ThreadPoolTimer ^ source)
	{
		this->UpdateLoudness(0);
	}));
	*/
}

void LoudnessCapture::UpdateLoudness(int value)
{
	this->m_loudness = value;
}