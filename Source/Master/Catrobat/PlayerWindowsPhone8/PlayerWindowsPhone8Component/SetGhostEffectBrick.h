#pragma once
#include "brick.h"
class SetGhostEffectBrick :
	public Brick
{
public:
	SetGhostEffectBrick(string spriteReference, FormulaTree *transparency, Script *parent);
	void Execute();
private:
	FormulaTree *m_transparency;
};
