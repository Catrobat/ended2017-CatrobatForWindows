#pragma once
#include "brick.h"
class PlaySoundBrick :
	public Brick
{
public:
	PlaySoundBrick(string spriteReference, string filename, string name);

private:
	string m_filename;
	string m_name;
};

