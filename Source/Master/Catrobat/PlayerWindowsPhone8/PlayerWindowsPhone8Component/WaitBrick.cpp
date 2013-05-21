#include "pch.h"
#include "WaitBrick.h"
#include <windows.h>
#include <ppltasks.h>

WaitBrick::WaitBrick(string objectReference, FormulaTree *timeToWaitInSeconds, Script *parent) :
	Brick(TypeOfBrick::WaitBrick, objectReference, parent), m_timeToWaitInSeconds(timeToWaitInSeconds)
{
}

void WaitBrick::Execute()
{
	Concurrency::wait(1000);
}