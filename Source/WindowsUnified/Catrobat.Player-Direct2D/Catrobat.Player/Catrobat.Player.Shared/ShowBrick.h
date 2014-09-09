#pragma once

#include "Brick.h"

class ShowBrick :
	public Brick
{
public:
	ShowBrick(Script *parent);
	void Execute();
};
