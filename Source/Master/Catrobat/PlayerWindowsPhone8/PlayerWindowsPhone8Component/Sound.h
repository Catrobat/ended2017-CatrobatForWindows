#pragma once

#include "fmod.hpp"
#include "fmod_errors.h"

class Sound
{
private:
	FMOD::Sound *sound;
	FMOD::System *system;
	FMOD::Channel *channel;
public:
	Sound::Sound(FMOD::System *system, FMOD::Channel *channel);
	~Sound(void);
	void Load();
	void Release();
	void Play();
};

