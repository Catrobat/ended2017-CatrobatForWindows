#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class PlaceAtBrick :
		public Brick
	{
	public:
		PlaceAtBrick(FormulaTree *positionX, FormulaTree *positionY, std::shared_ptr<Script> parent);
		void Execute();
	private:
		FormulaTree *m_positionX;
		FormulaTree *m_positionY;
	};
}