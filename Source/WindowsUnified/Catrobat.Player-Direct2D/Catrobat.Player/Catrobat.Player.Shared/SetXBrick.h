#pragma once

#include "Brick.h"

class SetXBrick :
	public Brick
{
public:
	SetXBrick(FormulaTree *m_positionX, Script *parent);
	void Execute();
private:
	FormulaTree *m_positionX;
};
