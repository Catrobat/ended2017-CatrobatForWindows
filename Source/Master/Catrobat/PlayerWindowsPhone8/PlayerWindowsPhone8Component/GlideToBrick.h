#pragma once
#include "brick.h"
class GlideToBrick :
	public Brick
{
public:
	GlideToBrick(string spriteReference, FormulaTree *xDestination, FormulaTree *yDestination, FormulaTree *duration, Script *parent);
	void Execute();
private:
	FormulaTree *m_xDestination;
	FormulaTree *m_yDestination;
	FormulaTree *m_duration;
};
