#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class ChangeGhostEffectByBrick :
		public Brick
	{
	public:
		ChangeGhostEffectByBrick(FormulaTree *transparency, std::shared_ptr<Script> parent);
		void Execute();
	private:
		FormulaTree *m_transparency;
	};
}