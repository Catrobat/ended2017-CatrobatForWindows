#pragma once
#include "Brick.h"
class ShowBrick :
	public Brick
{
public:
	ShowBrick(string spriteReference, Script *parent);
	void Execute();
};
