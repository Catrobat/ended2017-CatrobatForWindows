#pragma once
#include "Brick.h"
class PlaceAtBrick :
	public Brick
{
public:
	PlaceAtBrick(string spriteReference, FormulaTree *positionX, FormulaTree *positionY, Script *parent);
	void Execute();
private:
	FormulaTree *m_positionX;
	FormulaTree *m_positionY;
};
