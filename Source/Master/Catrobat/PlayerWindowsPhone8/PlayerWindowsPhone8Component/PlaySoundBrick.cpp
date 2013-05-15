#include "pch.h"
#include "PlaySoundBrick.h"
#include "SoundManager.h"

PlaySoundBrick::PlaySoundBrick(string spriteReference, string filename, string name, Script *parent) :
	Brick(TypeOfBrick::PlaySoundBrick, spriteReference, parent),
	m_filename(filename), m_name(name)
{
}

void PlaySoundBrick::Execute()
{
	SoundManager::Instance()->CreateOrGetSound(m_filename)->Play();
}