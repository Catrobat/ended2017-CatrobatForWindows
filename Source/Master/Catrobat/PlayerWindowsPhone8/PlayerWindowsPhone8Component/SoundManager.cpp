#include "pch.h"
#include "SoundManager.h"
#include "fmod.hpp"
#include "fmod_errors.h"
#include <map>

using namespace std;

SoundManager *SoundManager::__instance = NULL;

SoundManager *SoundManager::Instance()
{
	if (!__instance)
		__instance = new SoundManager();
	return __instance;
}

SoundManager::SoundManager(void)
{
	channel = 0;
	extradriverdata = 0;
	m_sounds = new map<string, Sound*>();
}

void SoundManager::Initialize()
{
    status = FMOD::System_Create(&system);
    status = system->getVersion(&version);
    FMOD_SPEAKERMODE speakerMode = FMOD_SPEAKERMODE_STEREO;
    status = system->getDriverCaps(0, NULL, NULL, &speakerMode);
    status = system->setSpeakerMode(speakerMode);
    status = system->init(32, FMOD_INIT_NORMAL, extradriverdata);
}

SoundManager::~SoundManager(void)
{
}

Sound* SoundManager::CreateOrGetSound(std::string filename)
{
	map<string, Sound*>::iterator currentSound;
	currentSound = m_sounds->find(filename);
	
	if (currentSound == m_sounds->end())
	{
		Sound* result = new Sound(system, channel);
		result->Load(filename);
		return result;
	}
	else
	{
		return currentSound->second;
	}
}