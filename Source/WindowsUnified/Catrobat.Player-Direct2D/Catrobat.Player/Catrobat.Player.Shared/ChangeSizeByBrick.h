#pragma once

#include "Brick.h"
#include "FormulaTree.h"

class ChangeSizeByBrick :
	public Brick
{
public:
	ChangeSizeByBrick(FormulaTree *scale, std::shared_ptr<Script> parent);
	void Execute();
private:
	FormulaTree *m_scale;
};
