#include "pch.h"
#include "Sound.h"

Sound::Sound(FMOD::System *system, FMOD::Channel *channel)
{
	this->system = system;
	this->channel = channel;
}

Sound::~Sound(void)
{
}

void Sound::Load()
{
	// Currently loads the same file every time.
	system->createSound("ms-appx:///wave.mp3", FMOD_HARDWARE, 0, &sound);
}

void Sound::Release()
{
	sound->release();
}

void Sound::Play()
{
	system->playSound(FMOD_CHANNEL_FREE, sound, false, &channel);
}