#pragma once

#include "Brick.h"

#include <string>

class ForeverBrick : 
	public Brick
{
public:
	ForeverBrick(std::string spriteReference, Script *parent);

	void Execute();

private:
};

