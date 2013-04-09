#pragma once
#include "brick.h"
#include "Script.h"
class WaitBrick :
	public Brick
{
public:
	WaitBrick(string spriteReference, Script* parent, int timeToWaitInMilliSeconds);

private:
	int m_timeToWaitInMilliSeconds;
};

