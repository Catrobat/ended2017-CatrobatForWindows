#pragma once

#include "Brick.h"

class GlideToBrick :
	public Brick
{
public:
	GlideToBrick(FormulaTree *xDestination, FormulaTree *yDestination, FormulaTree *duration, std::shared_ptr<Script> parent);
	void Execute();
private:
	FormulaTree *m_xDestination;
	FormulaTree *m_yDestination;
	FormulaTree *m_duration;
};
