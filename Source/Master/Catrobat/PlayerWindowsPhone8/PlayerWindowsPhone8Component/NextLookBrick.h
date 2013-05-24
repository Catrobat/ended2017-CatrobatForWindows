#pragma once
#include "brick.h"
class NextLookBrick :
	public Brick
{
public:
	NextLookBrick(string spriteReference, Script *parent);
	void Execute();
};
