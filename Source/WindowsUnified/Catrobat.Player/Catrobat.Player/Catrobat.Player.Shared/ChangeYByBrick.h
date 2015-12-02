#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class ChangeYByBrick :
		public Brick
	{
	public:
		ChangeYByBrick(FormulaTree *offsetY, Script* parent);
		void Execute();
	private:
		FormulaTree *m_offsetY;
	};
}