#pragma once

#include "Brick.h"

class NextLookBrick :
	public Brick
{
public:
	NextLookBrick(std::shared_ptr<Script> parent);
	void Execute();
};
