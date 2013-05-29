#pragma once
#include "brick.h"
class SetYBrick :
	public Brick
{
public:
	SetYBrick(string spriteReference, FormulaTree *m_positionY, Script *parent);
	void Execute();
private:
	FormulaTree *m_positionY;
};
