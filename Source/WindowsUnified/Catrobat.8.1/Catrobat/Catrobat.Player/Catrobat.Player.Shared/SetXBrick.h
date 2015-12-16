#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class SetXBrick :
		public Brick
	{
	public:
		SetXBrick(FormulaTree *m_positionX, Script* parent);
		void Execute();
	private:
		FormulaTree *m_positionX;
	};
}