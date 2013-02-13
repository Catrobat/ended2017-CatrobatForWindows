#include "pch.h"
#include "SoundManager.h"
#include "fmod.hpp"
#include "fmod_errors.h"

SoundManager::SoundManager(void)
{
	channel = 0;
	extradriverdata = 0;
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
