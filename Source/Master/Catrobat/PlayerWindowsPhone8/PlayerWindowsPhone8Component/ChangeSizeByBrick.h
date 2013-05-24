#pragma once
#include "brick.h"
#include "FormulaTree.h"

class ChangeSizeByBrick :
	public Brick
{
public:
	ChangeSizeByBrick(string spriteReference, FormulaTree *scale, Script *parent);
	void Execute();
private:
	FormulaTree *m_scale;
};
