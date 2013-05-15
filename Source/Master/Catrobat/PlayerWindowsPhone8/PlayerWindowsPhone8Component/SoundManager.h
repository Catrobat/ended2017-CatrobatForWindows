#pragma once

#include "fmod.hpp"
#include "fmod_errors.h"
#include "Sound.h"
#include <map>
#include <string>

class SoundManager
{
public:
	void Initialize();
	Sound* CreateOrGetSound(std::string filename);

	static SoundManager *Instance();
private:
	SoundManager(void);
	~SoundManager(void);
	static SoundManager *__instance;

	std::map<std::string, Sound*> *m_sounds;

	FMOD::System     *system;
    FMOD::Channel    *channel;
    FMOD_RESULT       status;
    unsigned int      version;
    void             *extradriverdata;
};

