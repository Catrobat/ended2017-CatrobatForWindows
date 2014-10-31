#pragma once

#include "Brick.h"

class SetGhostEffectBrick :
	public Brick
{
public:
	SetGhostEffectBrick(FormulaTree *transparency, std::shared_ptr<Script> parent);
	void Execute();
private:
	FormulaTree *m_transparency;
};
