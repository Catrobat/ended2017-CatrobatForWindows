#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class SetGhostEffectBrick :
		public Brick
	{
	public:
		SetGhostEffectBrick(FormulaTree *transparency, Script* parent);
		void Execute();
	private:
		FormulaTree *m_transparency;
	};
}