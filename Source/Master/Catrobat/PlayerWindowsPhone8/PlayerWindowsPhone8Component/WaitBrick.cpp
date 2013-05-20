#include "pch.h"
#include "WaitBrick.h"
#include <windows.h>
#include <ppltasks.h>

WaitBrick::WaitBrick(string objectReference, int timeToWaitInMilliSeconds, Script *parent) :
	Brick(TypeOfBrick::WaitBrick, objectReference, parent), m_timeToWaitInSeconds(timeToWaitInMilliSeconds)
{
}

void WaitBrick::Execute()
{
	Concurrency::wait(m_timeToWaitInSeconds * 1000);
}