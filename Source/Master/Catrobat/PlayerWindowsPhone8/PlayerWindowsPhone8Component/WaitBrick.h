#pragma once
#include "brick.h"
#include "FormulaTree.h"

class WaitBrick :
	public Brick
{
public:
	WaitBrick(string objectReference, FormulaTree *timeToWaitInSeconds, Script *parent);
	void Execute();
private:
	FormulaTree *m_timeToWaitInSeconds;
};
