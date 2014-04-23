#include "pch.h"
#include "SoundManager.h"
#include "fmod.hpp"
#include "fmod_errors.h"
#include <map>

using namespace std;

SoundManager *SoundManager::__instance = NULL;

SoundManager *SoundManager::Instance()
{
#ifndef UNITTEST
	if (!__instance)
		__instance = new SoundManager();
	return __instance;
#else
	return NULL;
#endif
}

SoundManager::SoundManager(void)
{
#ifndef UNITTEST
	m_channel = 0;
	m_extradriverdata = 0;
	m_sounds = new map<string, Sound*>();
#endif
}

void SoundManager::Initialize()
{
#ifndef UNITTEST
    m_status = FMOD::System_Create(&m_system);
    m_status = m_system->getVersion(&m_version);
    FMOD_SPEAKERMODE speakerMode = FMOD_SPEAKERMODE_STEREO;
    m_status = m_system->getDriverCaps(0, NULL, NULL, &speakerMode);
    m_status = m_system->setSpeakerMode(speakerMode);
    m_status = m_system->init(32, FMOD_INIT_NORMAL, m_extradriverdata);
#endif
}

SoundManager::~SoundManager(void)
{
}

Sound* SoundManager::CreateOrGetSound(std::string filename)
{
#ifndef UNITTEST
	map<string, Sound*>::iterator currentSound;
	currentSound = m_sounds->find(filename);
	
	if (currentSound == m_sounds->end())
	{
		Sound* result = new Sound(m_system, m_channel);
		result->Load(filename);
		return result;
	}
	else
	{
		return currentSound->second;
	}
#else
	return NULL;
#endif
}