#include "pch.h"
#include "FmodMicrophoneProvider.h"

FmodMicrophoneProvider *FmodMicrophoneProvider::__instance = nullptr;

FmodMicrophoneProvider::FmodMicrophoneProvider()
{
#ifndef UNITTEST
	m_system = 0;
	m_sound = 0;
	m_channel = 0;

	Initialize();
#endif
}

FmodMicrophoneProvider* FmodMicrophoneProvider::Instance()
{
#ifndef UNITTEST
	if (!__instance)
		__instance = new FmodMicrophoneProvider();
	return __instance;
#else
	return NULL;
#endif
}

void FmodMicrophoneProvider::Initialize()
{
#ifndef UNITTEST
	m_status = FMOD::System_Create(&m_system);
	ErrorCheck();

	m_status = m_system->getVersion(&m_version);
	ErrorCheck();

	if (m_version < FMOD_VERSION)
	{
		throw "FmodMicrophoneProvider: wrong FMOD version!";
	}

	/// get sound driver
	/*
	m_status = m_system->getNumDrivers(&m_numdrivers);
	ErrorCheck();

	if (m_numdrivers == 0)
	{
		m_status = m_system->setOutput(FMOD_OUTPUTTYPE_NOSOUND);
		ErrorCheck();
	}
	else
	{
		m_status = m_system->getDriverCaps(0, &m_caps, 0, &m_speakerMode);
		ErrorCheck();

		m_status = m_system->setSpeakerMode(m_speakerMode);
		ErrorCheck();

		if (m_caps & FMOD_CAPS_HARDWARE_EMULATED)
		{
			m_status = m_system->setDSPBufferSize(1024, 10);
			ErrorCheck();
		}

		m_status = m_system->getDriverInfo(0, m_nameSoundDriver, 256, 0);
		ErrorCheck();

		if (strstr(m_nameSoundDriver, "SigmaTel"))
		{
			m_status = m_system->setSoftwareFormat(48000, FMOD_SOUND_FORMAT_PCMFLOAT, 0, 0, FMOD_DSP_RESAMPLER_LINEAR);
			ErrorCheck();
		}
	}
	*/
	///get record driver

	m_status = m_system->getRecordNumDrivers(&m_numRecordDriver);
	ErrorCheck();

	m_driverRecord = 0;

	if (m_numRecordDriver == 0)
	{
		//TODO: change!
		throw "fmod: no microphone";
	}
	else
	{
		m_status = m_system->getRecordDriverInfo(m_numRecordDriver, m_nameRecordDriver, 256, 0);
		ErrorCheck();

		m_driverRecord = m_numRecordDriver;
	}

	/// init

	m_status = m_system->init(100, FMOD_INIT_NORMAL, 0);

	if (m_status == FMOD_ERR_OUTPUT_CREATEBUFFER)
	{
		m_status = m_system->setSpeakerMode(FMOD_SPEAKERMODE_STEREO);
		ErrorCheck();

		m_status = m_system->init(100, FMOD_INIT_NORMAL, 0);
	}
	ErrorCheck();

	memset(&m_exinfo, 0, sizeof(FMOD_CREATESOUNDEXINFO));

	m_exinfo.cbsize = sizeof(FMOD_CREATESOUNDEXINFO);
	m_exinfo.numchannels = 1;
	m_exinfo.format = FMOD_SOUND_FORMAT_PCM16;
	m_exinfo.defaultfrequency = 44100;
	m_exinfo.length = m_exinfo.defaultfrequency * sizeof(short)* m_exinfo.numchannels * 5;

	m_status = m_system->createSound(0, FMOD_2D | FMOD_SOFTWARE | FMOD_OPENUSER, &m_exinfo, &m_sound);
	ErrorCheck();
#endif
}

void FmodMicrophoneProvider::StartCapture()
{
#ifndef UNITTEST
	// create sound buffer

	m_system->recordStart(m_driverRecord, m_sound, true);

	// sleep(60)

	m_system->playSound(FMOD_CHANNEL_FREE, m_sound, false, &m_channel);
	m_channel->setVolume(0);
#endif
}

void FmodMicrophoneProvider::ErrorCheck()
{
#ifndef UNITTEST
	if (m_status != FMOD_OK)
	{
		throw "FMOD error in FmodMicrophoneProvider!";
	}
#endif
}

double FmodMicrophoneProvider::StartRecordingFiveSeconds()
{
#ifndef UNITTEST
	m_status = m_system->recordStart(m_driverRecord, m_sound, false);
	ErrorCheck();
	return 1;
#else
	return 0;
#endif
}

void FmodMicrophoneProvider::PlayRecordedFiveSeconds()
{
#ifndef UNITTEST
	m_sound->setMode(FMOD_LOOP_OFF);

	m_status = m_system->playSound(FMOD_CHANNEL_REUSE, m_sound, false, &m_channel);
	ErrorCheck();
#endif
}

#if defined(WIN32) || defined(__WATCOMC__) || defined(_WIN32) || defined(__WIN32__)
#define __PACKED                         /* dummy */
#else
#define __PACKED __attribute__((packed)) /* gcc packed */
#endif

void FmodMicrophoneProvider::SaveToWave()
{
#ifndef UNITTEST
	FILE *fp;
	int             channels, bits;
	float           rate;
	void           *ptr1, *ptr2;
	unsigned int    lenbytes, len1, len2;

	if (!m_sound)
	{
		return;
	}

	m_sound->getFormat(0, 0, &channels, &bits);
	m_sound->getDefaults(&rate, 0, 0, 0);
	m_sound->getLength(&lenbytes, FMOD_TIMEUNIT_PCMBYTES);

	{
#if defined(WIN32) || defined(_WIN64) || defined(__WATCOMC__) || defined(_WIN32) || defined(__WIN32__)
#pragma pack(1)
#endif

		/*
		WAV Structures
		*/
		typedef struct
		{
			signed char id[4];
			int 		size;
		} RiffChunk;

		struct
		{
			RiffChunk       chunk           __PACKED;
			unsigned short	wFormatTag      __PACKED;    /* format type  */
			unsigned short	nChannels       __PACKED;    /* number of channels (i.e. mono, stereo...)  */
			unsigned int	nSamplesPerSec  __PACKED;    /* sample rate  */
			unsigned int	nAvgBytesPerSec __PACKED;    /* for buffer estimation  */
			unsigned short	nBlockAlign     __PACKED;    /* block size of data  */
			unsigned short	wBitsPerSample  __PACKED;    /* number of bits per sample of mono data */
		} FmtChunk = { { { 'f', 'm', 't', ' ' }, sizeof(FmtChunk)-sizeof(RiffChunk) }, 1, channels, (int)rate, (int)rate * channels * bits / 8, 1 * channels * bits / 8, bits } __PACKED;

		struct
		{
			RiffChunk   chunk;
		} DataChunk = { { { 'd', 'a', 't', 'a' }, lenbytes } };

		struct
		{
			RiffChunk   chunk;
			signed char rifftype[4];
		} WavHeader = { { { 'R', 'I', 'F', 'F' }, sizeof(FmtChunk)+sizeof(RiffChunk)+lenbytes }, { 'W', 'A', 'V', 'E' } };

#if defined(WIN32) || defined(_WIN64) || defined(__WATCOMC__) || defined(_WIN32) || defined(__WIN32__)
#pragma pack()
#endif

		fp = _fsopen("record.wav", "w", _SH_DENYWR);

		/*
		Write out the WAV header.
		*/
		fwrite(&WavHeader, sizeof(WavHeader), 1, fp);
		fwrite(&FmtChunk, sizeof(FmtChunk), 1, fp);
		fwrite(&DataChunk, sizeof(DataChunk), 1, fp);

		/*
		Lock the sound to get access to the raw data.
		*/
		m_sound->lock(0, lenbytes, &ptr1, &ptr2, &len1, &len2);

		/*
		Write it to disk.
		*/
		fwrite(ptr1, len1, 1, fp);

		/*
		Unlock the sound to allow FMOD to use it again.
		*/
		m_sound->unlock(ptr1, ptr2, len1, len2);

		fclose(fp);
	}
#endif
}

