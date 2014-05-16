#pragma once

#include "WASAPIAudio.h"

ref class LoudnessCapture sealed
{
public:

	LoudnessCapture();

	double GetLoudness();
	bool StartCapture();
	bool StopCapture();

private:

	double m_loudness;
	WasapiComp::WASAPIAudio ^m_wasapiAudio;

	bool m_isRecording;
};

