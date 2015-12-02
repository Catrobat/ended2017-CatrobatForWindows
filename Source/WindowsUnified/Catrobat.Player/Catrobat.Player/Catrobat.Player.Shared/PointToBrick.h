#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class PointToBrick :
		public Brick
	{
	public:
		PointToBrick(FormulaTree *rotation, std::shared_ptr<Script> parent);
		void Execute();
	private:
		FormulaTree *m_rotation;
	};
}