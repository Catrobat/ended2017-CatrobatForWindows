#pragma once
#include "Brick.h"
class SetYBrick :
	public Brick
{
public:
	SetYBrick(string spriteReference, FormulaTree *positionY, Script *parent);
	void Execute();
private:
	FormulaTree *m_positionY;
};
