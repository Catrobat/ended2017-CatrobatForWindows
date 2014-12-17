#pragma once

#include "Brick.h"

class SetXBrick :
	public Brick
{
public:
	SetXBrick(FormulaTree *m_positionX, std::shared_ptr<Script> parent);
	void Execute();
private:
	FormulaTree *m_positionX;
};
