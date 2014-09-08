#pragma once

#include "Brick.h"

class SetSizeToBrick :
	public Brick
{
public:
	SetSizeToBrick(FormulaTree *scale, Script *parent);
	void Execute();
private:
	FormulaTree *m_scale;
};
