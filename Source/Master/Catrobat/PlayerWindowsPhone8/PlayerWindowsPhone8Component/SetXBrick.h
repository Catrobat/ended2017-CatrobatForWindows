#pragma once
#include "brick.h"
class SetXBrick :
	public Brick
{
public:
	SetXBrick(string spriteReference, FormulaTree *m_positionX, Script *parent);
	void Execute();
private:
	FormulaTree *m_positionX;
};
