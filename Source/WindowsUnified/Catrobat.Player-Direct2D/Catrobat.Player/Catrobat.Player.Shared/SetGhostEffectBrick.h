#pragma once
#include "Brick.h"
class SetGhostEffectBrick :
	public Brick
{
public:
	SetGhostEffectBrick(FormulaTree *transparency, Script *parent);
	void Execute();
private:
	FormulaTree *m_transparency;
};
