#include "pch.h"
#include "WaitBrick.h"
#include <windows.h>
#include <ppltasks.h>

WaitBrick::WaitBrick(string spriteReference, int timeToWaitInMilliSeconds, Script *parent) :
	Brick(TypeOfBrick::WaitBrick, spriteReference, parent), m_timeToWaitInSeconds(timeToWaitInMilliSeconds)
{
}

void WaitBrick::Execute()
{
	Concurrency::wait(m_timeToWaitInSeconds * 1000);
}