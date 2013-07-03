#pragma once
#include "Brick.h"
class HideBrick :
	public Brick
{
public:
	HideBrick(string spriteReference, Script *parent);
	void Execute();
};
