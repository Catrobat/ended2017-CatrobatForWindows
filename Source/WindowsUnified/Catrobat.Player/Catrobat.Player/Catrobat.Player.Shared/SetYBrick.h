#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class SetYBrick :
		public Brick
	{
	public:
		SetYBrick(FormulaTree *positionY, std::shared_ptr<Script> parent);
		void Execute();
	private:
		FormulaTree *m_positionY;
	};
}