#include "pch.h"
#include "WaitBrick.h"
#include "Script.h"

WaitBrick::WaitBrick(string spriteReference, Script* parent, int timeToWaitInMilliSeconds) :
	Brick(TypeOfBrick::WaitBrick, spriteReference, parent), m_timeToWaitInMilliSeconds(timeToWaitInMilliSeconds)
{
}

