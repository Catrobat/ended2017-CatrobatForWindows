#pragma once

//TODO: FMOD actually didn't work on WP81; change to use DXTK Lib

//#include "fmod.hpp"
//#include "fmod_errors.h"
#include <string>

class Sound
{
private:
	//FMOD::Sound *m_sound;
	//FMOD::System *m_system;
	//FMOD::Channel *m_channel;
public:
	//Sound::Sound(FMOD::System *system, FMOD::Channel *channel);
	~Sound(void);
	void Load(std::string filename);
	void Release();
	void Play();
};

