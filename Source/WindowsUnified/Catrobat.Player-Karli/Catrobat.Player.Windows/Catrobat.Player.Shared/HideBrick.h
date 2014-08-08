#pragma once
#include "Brick.h"
class HideBrick :
	public Brick
{
public:
	HideBrick(Script *parent);
	void Execute();
};
