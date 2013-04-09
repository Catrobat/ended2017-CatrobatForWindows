#pragma once
#include "brick.h"
#include "Script.h"
class PlaySoundBrick :
	public Brick
{
public:
	PlaySoundBrick(string spriteReference, Script* parent, string filename, string name);

private:
	string m_filename;
	string m_name;
};

