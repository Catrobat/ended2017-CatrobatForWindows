#include "pch.h"
#include "PlaySoundBrick.h"
#include "Script.h"


PlaySoundBrick::PlaySoundBrick(string spriteReference, Script* parent, string filename, string name) :
	Brick(TypeOfBrick::PlaySoundBrick, spriteReference, parent),
	m_filename(filename), m_name(name)
{
}
