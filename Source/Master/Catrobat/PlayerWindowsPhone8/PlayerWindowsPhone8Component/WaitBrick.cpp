#include "pch.h"
#include "WaitBrick.h"
#include "dos.h"

WaitBrick::WaitBrick(string spriteReference, int timeToWaitInMilliSeconds, Script *parent) :
	Brick(TypeOfBrick::WaitBrick, spriteReference, parent), m_timeToWaitInMilliSeconds(timeToWaitInMilliSeconds)
{
}

void WaitBrick::Execute()
{
	//sleep(1);
}