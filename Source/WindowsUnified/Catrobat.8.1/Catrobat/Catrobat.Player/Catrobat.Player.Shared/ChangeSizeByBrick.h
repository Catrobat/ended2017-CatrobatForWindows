#pragma once

#include "Brick.h"
#include "FormulaTree.h"

namespace ProjectStructure
{
	class ChangeSizeByBrick :
		public Brick
	{
	public:
		ChangeSizeByBrick(FormulaTree *scale, Script* parent);
		void Execute();
	private:
		FormulaTree *m_scale;
	};

}