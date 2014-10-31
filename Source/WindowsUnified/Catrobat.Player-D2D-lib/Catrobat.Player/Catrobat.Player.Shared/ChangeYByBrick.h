#pragma once

#include "Brick.h"

class ChangeYByBrick :
	public Brick
{
public:
	ChangeYByBrick(FormulaTree *offsetY, std::shared_ptr<Script> parent);
	void Execute();
private:
	FormulaTree *m_offsetY;
};
