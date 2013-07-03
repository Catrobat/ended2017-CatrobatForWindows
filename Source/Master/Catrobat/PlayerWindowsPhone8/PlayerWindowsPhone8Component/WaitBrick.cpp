#include "pch.h"
#include "WaitBrick.h"
#include <windows.h>
#include <ppltasks.h>
#include "Interpreter.h"

WaitBrick::WaitBrick(string objectReference, FormulaTree *timeToWaitInSeconds, Script *parent) :
	Brick(TypeOfBrick::WaitBrick, objectReference, parent), m_timeToWaitInSeconds(timeToWaitInSeconds)
{
}

void WaitBrick::Execute()
{
	Concurrency::wait(1000 * Interpreter::Instance()->EvaluateFormulaToInt(m_timeToWaitInSeconds, Parent()->Parent()));
}