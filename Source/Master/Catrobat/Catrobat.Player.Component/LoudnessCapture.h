#pragma once

#include "WASAPIAudio.h"
#include <ppltasks.h>

ref class LoudnessCapture sealed
{
public:

	LoudnessCapture();

	double GetLoudness();
	bool StartCapture();
	bool StopCapture();

	void UpdateLoudness(int value);

private:

	double m_loudness;
	WasapiComp::WASAPIAudio ^m_wasapiAudio;
	bool m_isRecording;
	
	void StartPeriodicCalculationLoudness();
	Windows::System::Threading::ThreadPoolTimer^ m_timer;
};

