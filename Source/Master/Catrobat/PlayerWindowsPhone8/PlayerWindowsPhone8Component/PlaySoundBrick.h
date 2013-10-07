#pragma once
#include "Brick.h"
class PlaySoundBrick :
	public Brick
{
public:
	PlaySoundBrick(string filename, string name, Script *parent);
	void Execute();
private:
	string m_filename;
	string m_name;
};
