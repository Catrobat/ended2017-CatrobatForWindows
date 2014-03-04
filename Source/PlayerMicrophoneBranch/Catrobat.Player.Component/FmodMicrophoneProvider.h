#pragma once

#include "fmod.hpp"
#include "fmod_errors.h"

using namespace FMOD;

class FmodMicrophoneProvider 
{
public:

	static FmodMicrophoneProvider* Instance();
	double StartRecordingFiveSeconds();
	void PlayRecordedFiveSeconds();

private:

	FmodMicrophoneProvider();
	static FmodMicrophoneProvider* __instance;

	FMOD::System *m_system;
	FMOD::Sound *m_sound;
	FMOD::Channel *m_channel;
	FMOD_RESULT m_status;
	FMOD_CREATESOUNDEXINFO m_exinfo;
	FMOD_SPEAKERMODE m_speakerMode;
	FMOD_CAPS m_caps;
	int m_key;
	int m_driver;
	int m_driverRecord;
	int m_numdrivers;
	int m_numRecordDriver;
	int m_count;
	unsigned int m_version;
	char m_nameSoundDriver[256];
	char m_nameRecordDriver[256];

	static int const m_SAMPLERATE = 44100;
	static int const m_CHANNELS = 2;

	int m_recordingSources;

	void StartCapture();
	void StopCapture();

	void ErrorCheck();
	void Initialize();
};


