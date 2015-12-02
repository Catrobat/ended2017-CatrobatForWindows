#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class NextLookBrick :
		public Brick
	{
	public:
		NextLookBrick(std::shared_ptr<Script> parent);
		void Execute();
	};

}