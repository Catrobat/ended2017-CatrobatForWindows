#pragma once

#include "Brick.h"

class TurnRightBrick :
	public Brick
{
public:
	TurnRightBrick(FormulaTree *rotation, std::shared_ptr<Script> parent);
	void Execute();
private:
	FormulaTree *m_rotation;
};
