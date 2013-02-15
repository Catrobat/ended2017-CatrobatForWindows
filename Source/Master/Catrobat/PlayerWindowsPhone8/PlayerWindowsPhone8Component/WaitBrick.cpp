#include "pch.h"
#include "WaitBrick.h"


WaitBrick::WaitBrick(string spriteReference, int timeToWaitInMilliSeconds) :
	Brick(TypeOfBrick::WaitBrick, spriteReference), m_timeToWaitInMilliSeconds(timeToWaitInMilliSeconds)
{
}

