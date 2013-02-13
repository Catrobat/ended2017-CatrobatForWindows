#pragma once

#include "fmod.hpp"
#include "fmod_errors.h"
#include "Sound.h"

class SoundManager
{
public:
	SoundManager(void);
	~SoundManager(void);
	void Initialize();
	Sound* CreateSound();
private:
	FMOD::System     *system;
    FMOD::Channel    *channel;
    FMOD_RESULT       status;
    unsigned int      version;
    void             *extradriverdata;
};

