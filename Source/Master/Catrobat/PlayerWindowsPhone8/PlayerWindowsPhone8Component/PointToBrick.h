#pragma once
#include "Brick.h"
class PointToBrick :
	public Brick
{
public:
	PointToBrick(string spriteReference, FormulaTree *rotation, Script *parent);
	void Execute();
private:
	FormulaTree *m_rotation;
};
