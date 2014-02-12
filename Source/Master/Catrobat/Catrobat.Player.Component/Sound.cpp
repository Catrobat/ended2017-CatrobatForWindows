#include "pch.h"
#include "Sound.h"
#include "ProjectDaemon.h"
#include <string>

Sound::Sound(FMOD::System *system, FMOD::Channel *channel)
{
#ifndef UNITTEST
	m_system = system;
	m_channel = channel;
#endif
}

Sound::~Sound(void)
{
}

void Sound::Load(std::string filename)
{
#ifndef UNITTEST
	// Currently loads the same file every time.
	m_system->createSound((ProjectDaemon::Instance()->GetProjectPath() + "/sounds/" + filename).c_str(), FMOD_HARDWARE, 0, &m_sound);
#endif
}

void Sound::Release()
{
#ifndef UNITTEST
	m_sound->release();
#endif
}

void Sound::Play()
{
#ifndef UNITTEST
	m_system->playSound(FMOD_CHANNEL_FREE, m_sound, false, &m_channel);
#endif
}

