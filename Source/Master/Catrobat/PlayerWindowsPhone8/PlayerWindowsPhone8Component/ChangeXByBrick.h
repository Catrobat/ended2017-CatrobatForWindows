#pragma once
#include "Brick.h"
class ChangeXByBrick :
	public Brick
{
public:
	ChangeXByBrick(string spriteReference, FormulaTree *offsetX, Script *parent);
	void Execute();
private:
	FormulaTree *m_offsetX;
};
