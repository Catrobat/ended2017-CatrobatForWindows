#include "pch.h"
#include "PlaySoundBrick.h"
#include "Helper.h"
#include "SoundManager.h"

#include <thread>

using namespace std;
using namespace ProjectStructure;

PlaySoundBrick::PlaySoundBrick(Catrobat_Player::NativeComponent::IPlaySoundBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::PlaySoundBrick, parent),
	m_filename(Helper::StdString(brick->FileName)),
	m_name(Helper::StdString(brick->Name))
{
}

void PlaySoundBrick::Execute()
{
	thread t1(&SoundManager::Play, SoundManager::Instance(), m_filename);
}