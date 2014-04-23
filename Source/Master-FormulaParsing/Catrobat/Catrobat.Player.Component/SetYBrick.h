#pragma once
#include "Brick.h"
class SetYBrick :
	public Brick
{
public:
	SetYBrick(FormulaTree *positionY, Script *parent);
	void Execute();
private:
	FormulaTree *m_positionY;
};
