#include "pch.h"
#include "WaitBrick.h"


WaitBrick::WaitBrick(string spriteReference, int timeToWaitInMilliSeconds, Script *parent) :
	Brick(TypeOfBrick::WaitBrick, spriteReference, parent), m_timeToWaitInMilliSeconds(timeToWaitInMilliSeconds)
{
}

