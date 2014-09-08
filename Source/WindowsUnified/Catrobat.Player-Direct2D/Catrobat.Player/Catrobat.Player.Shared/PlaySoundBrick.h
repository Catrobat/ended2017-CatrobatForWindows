#pragma once

#include "Brick.h"

class PlaySoundBrick :
	public Brick
{
public:
	PlaySoundBrick(std::string filename, std::string name, Script *parent);
	void Execute();
private:
	std::string m_filename;
	std::string m_name;
};
