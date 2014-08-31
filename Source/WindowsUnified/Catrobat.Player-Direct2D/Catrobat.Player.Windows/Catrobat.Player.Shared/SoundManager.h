#pragma once

//TODO: FMOD actually didn't work on WP81; change to use DXTK Lib

//#include "fmod.hpp"
//#include "fmod_errors.h"
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

	//FMOD::System     *m_system;
 //   FMOD::Channel    *m_channel;
 //   FMOD_RESULT       m_status;
    unsigned int      m_version;
    void             *m_extradriverdata;
};

