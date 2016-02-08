#include "pch.h"
#include "PlaySoundBrick.h"
#include "Helper.h"

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
	//SoundManager::Instance()->CreateOrGetSound(m_filename)->Play();
}