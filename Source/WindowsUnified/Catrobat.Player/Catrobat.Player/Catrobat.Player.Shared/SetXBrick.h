#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class SetXBrick :
		public Brick
	{
	public:
		SetXBrick(FormulaTree *m_positionX, std::shared_ptr<Script> parent);
		void Execute();
	private:
		FormulaTree *m_positionX;
	};
}