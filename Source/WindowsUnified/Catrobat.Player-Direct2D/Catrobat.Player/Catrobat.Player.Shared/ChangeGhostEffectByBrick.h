#pragma once

#include "Brick.h"

class ChangeGhostEffectByBrick :
	public Brick
{
public:
	ChangeGhostEffectByBrick(FormulaTree *transparency, Script *parent);
	void Execute();
private:
	FormulaTree *m_transparency;
};
