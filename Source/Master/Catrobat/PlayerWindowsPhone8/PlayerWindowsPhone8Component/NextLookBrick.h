#pragma once
#include "Brick.h"
class NextLookBrick :
	public Brick
{
public:
	NextLookBrick(string spriteReference, Script *parent);
	void Execute();
};
