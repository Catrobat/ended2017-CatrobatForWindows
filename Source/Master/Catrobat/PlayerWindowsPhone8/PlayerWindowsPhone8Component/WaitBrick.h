#pragma once
#include "brick.h"
class WaitBrick :
	public Brick
{
public:
	WaitBrick(string spriteReference, int timeToWaitInMilliSeconds, Script *parent);
	void Execute();
private:
	int m_timeToWaitInMilliSeconds;
};
