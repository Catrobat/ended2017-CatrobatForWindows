#include "pch.h"
#include "Sound.h"
#include "ProjectDaemon.h"
#include <string>

Sound::Sound(FMOD::System *system, FMOD::Channel *channel)
{
	this->system = system;
	this->channel = channel;
}

Sound::~Sound(void)
{
}

void Sound::Load(std::string filename)
{
	// Currently loads the same file every time.
	system->createSound((ProjectDaemon::Instance()->ProjectPath() + "/sounds/" + filename).c_str(), FMOD_HARDWARE, 0, &sound);
}

void Sound::Release()
{
	sound->release();
}

void Sound::Play()
{
	system->playSound(FMOD_CHANNEL_FREE, sound, false, &channel);
}

