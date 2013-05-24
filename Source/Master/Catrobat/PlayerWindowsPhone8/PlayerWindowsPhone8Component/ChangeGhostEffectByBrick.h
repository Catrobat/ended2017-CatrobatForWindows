#pragma once
#include "brick.h"
class ChangeGhostEffectByBrick :
	public Brick
{
public:
	ChangeGhostEffectByBrick(string spriteReference, FormulaTree *transparency, Script *parent);
	void Execute();
private:
	FormulaTree *m_transparency;
};
