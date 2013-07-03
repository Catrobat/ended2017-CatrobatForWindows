#pragma once
#include "Brick.h"
class TurnRightBrick :
	public Brick
{
public:
	TurnRightBrick(string spriteReference, FormulaTree *rotation, Script *parent);
	void Execute();
private:
	FormulaTree *m_rotation;
};
