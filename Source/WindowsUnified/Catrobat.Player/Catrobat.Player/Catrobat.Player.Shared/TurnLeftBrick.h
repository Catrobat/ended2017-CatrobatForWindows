#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class TurnLeftBrick :
		public Brick
	{
	public:
		TurnLeftBrick(FormulaTree *rotation, std::shared_ptr<Script> parent);
		void Execute();
	private:
		FormulaTree *m_rotation;
	};
}