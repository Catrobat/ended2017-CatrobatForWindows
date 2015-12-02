#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class SetGhostEffectBrick :
		public Brick
	{
	public:
		SetGhostEffectBrick(FormulaTree *transparency, std::shared_ptr<Script> parent);
		void Execute();
	private:
		FormulaTree *m_transparency;
	};
}