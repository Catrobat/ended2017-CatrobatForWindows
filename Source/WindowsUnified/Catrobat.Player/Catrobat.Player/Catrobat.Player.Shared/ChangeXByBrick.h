#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class ChangeXByBrick :
		public Brick
	{
	public:
		ChangeXByBrick(FormulaTree *offsetX, std::shared_ptr<Script> parent);
		void Execute();
	private:
		FormulaTree *m_offsetX;
	};

}