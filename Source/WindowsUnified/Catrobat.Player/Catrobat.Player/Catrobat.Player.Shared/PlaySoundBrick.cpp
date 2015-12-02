#include "pch.h"
#include "PlaySoundBrick.h"

using namespace std;
using namespace ProjectStructure;

PlaySoundBrick::PlaySoundBrick(string filename, string name, std::shared_ptr<Script>parent) :
	Brick(TypeOfBrick::PlaySoundBrick, parent),
	m_filename(filename), m_name(name)
{
}

void PlaySoundBrick::Execute()
{
	//SoundManager::Instance()->CreateOrGetSound(m_filename)->Play();
}