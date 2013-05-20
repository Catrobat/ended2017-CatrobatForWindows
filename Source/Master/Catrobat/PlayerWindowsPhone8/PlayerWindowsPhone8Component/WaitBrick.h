#pragma once
#include "brick.h"
class WaitBrick :
	public Brick
{
public:
	WaitBrick(string objectReference, int timeToWaitInSeconds, Script *parent);
	void Execute();
private:
	int m_timeToWaitInSeconds;
};
