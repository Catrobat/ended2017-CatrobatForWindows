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
	WasapiComp::WASAPIAudio^ wasapi = this->m_wasapiAudio;

	TimeSpan delay;
	delay.Duration = 10000000 / 2; // 500 milli sec

	m_timer = ThreadPoolTimer::CreatePeriodicTimer(
		ref new TimerElapsedHandler([this, wasapi](ThreadPoolTimer^ source)
	{
		Platform::Array<unsigned char, 1U>^ buffer = ref new Platform::Array<unsigned char, 1U>(0);
		int size = wasapi->ReadBytes(&buffer);

		long highest = 0;
		
		for (int i = 0; i < size; i++)
		{
			long value = buffer[i];
			value = value << 8;
			value += buffer[i + 1];
			
			if (value < 0)
				value *= -1;

			if (highest < value)
				highest = value;

			i++;
		}

		this->UpdateLoudness(highest);
	

	}), delay,
		ref new TimerDestroyedHandler([&](ThreadPoolTimer ^ source)
	{
		this->UpdateLoudness(0);
	}));
	
}

void LoudnessCapture::UpdateLoudness(int value)
{
	this->m_loudness = value;
}
