#include "pch.h"
#include "PlaySoundBrick.h"


PlaySoundBrick::PlaySoundBrick(string spriteReference, string filename, string name) :
	Brick(TypeOfBrick::PlaySoundBrick, spriteReference),
	m_filename(filename), m_name(name)
{
}
