#pragma once
#include "brick.h"
class ChangeYByBrick :
	public Brick
{
public:
	ChangeYByBrick(string spriteReference, FormulaTree *offsetY, Script *parent);
	void Execute();
private:
	FormulaTree *m_offsetY;
};
