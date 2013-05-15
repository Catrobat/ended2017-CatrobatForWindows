#pragma once

#include "fmod.hpp"
#include "fmod_errors.h"
#include <string>

class Sound
{
private:
	FMOD::Sound *sound;
	FMOD::System *system;
	FMOD::Channel *channel;
public:
	Sound::Sound(FMOD::System *system, FMOD::Channel *channel);
	~Sound(void);
	void Load(std::string filename);
	void Release();
	void Play();
};

