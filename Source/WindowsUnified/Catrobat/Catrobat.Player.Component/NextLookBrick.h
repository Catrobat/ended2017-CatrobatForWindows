#pragma once
#include "Brick.h"
class NextLookBrick :
	public Brick
{
public:
	NextLookBrick(Script *parent);
	void Execute();
};
