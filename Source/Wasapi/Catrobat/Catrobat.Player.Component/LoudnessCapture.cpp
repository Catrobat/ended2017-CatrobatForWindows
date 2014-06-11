#include "pch.h"
#include "LoudnessCapture.h"

#include "DeviceInformation.h"
#include "PlayerException.h"

#include <string.h>
#include <sstream>
#include <math.h>
using namespace std;

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
	if (DeviceInformation::IsRunningOnDevice())
	{
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
	}
	else
	{
		return false;
		//throw new PlayerException("init WASAPI failed, Microphone is not supported");
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
	delay.Duration = 10000000 / 4; 

	m_timer = ThreadPoolTimer::CreatePeriodicTimer(
		ref new TimerElapsedHandler([this, wasapi](ThreadPoolTimer^ source)
	{
		Platform::Array<byte>^ buffer = ref new Platform::Array<byte>(0); //TODO: change to byte array
		int size = wasapi->ReadBytes(&buffer);

		/*
		long highest = 0;
		
		for (int i = 0; i < size; )
		{
			unsigned long value1 = 0;
			unsigned long value2 = 0;
			unsigned long valuex = 0;

			valuex = buffer[i + 1];
			value1 = valuex << 8;
			value2 = buffer[i];

			unsigned long value = value1 + value2;
			
			if (value < 0)
				value *= -1;

			if (highest < value)
				highest = value;

			i += 2;
		}
		*/

		double rms = 0;
		unsigned short byte1 = 0;
		unsigned short byte2 = 0;
		short value = 0;
		int volume = 0;
		rms = (short)(byte1 | (byte2 << 8));

		for (int i = 0; i < size; i += 2)
		{
			byte1 = buffer[i];
			byte2 = buffer[i + 1];
			value = (short)(byte1 | (byte2 << 8));
			rms += std::pow(value, 2);
		}

		rms /= (double)(size / 2);
		volume = (int)std::floor(std::sqrt(rms));

		this->UpdateLoudness(volume);

	}), delay,
		ref new TimerDestroyedHandler([&](ThreadPoolTimer ^ source)
	{
		//this->UpdateLoudness(0);
	}));
	
}

void LoudnessCapture::UpdateLoudness(int value)
{
	this->m_loudness = value;
}
