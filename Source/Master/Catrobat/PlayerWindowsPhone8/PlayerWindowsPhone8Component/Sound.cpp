#include "pch.h"
#include "Sound.h"
#include "ProjectDaemon.h"
#include <string>

Sound::Sound(FMOD::System *system, FMOD::Channel *channel)
{
#ifndef UNITTEST
	this->system = system;
	this->channel = channel;
#endif
}

Sound::~Sound(void)
{
}

void Sound::Load(std::string filename)
{
#ifndef UNITTEST
	// Currently loads the same file every time.
	system->createSound((ProjectDaemon::Instance()->ProjectPath() + "/sounds/" + filename).c_str(), FMOD_HARDWARE, 0, &sound);
#endif
}

void Sound::Release()
{
#ifndef UNITTEST
	sound->release();
#endif
}

void Sound::Play()
{
#ifndef UNITTEST
	system->playSound(FMOD_CHANNEL_FREE, sound, false, &channel);
#endif
}

