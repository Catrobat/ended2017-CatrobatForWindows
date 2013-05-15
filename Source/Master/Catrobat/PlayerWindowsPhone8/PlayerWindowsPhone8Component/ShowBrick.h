#pragma once
#include "brick.h"
class ShowBrick :
	public Brick
{
public:
	ShowBrick(string spriteReference, Script *parent);
	void Execute();
};
