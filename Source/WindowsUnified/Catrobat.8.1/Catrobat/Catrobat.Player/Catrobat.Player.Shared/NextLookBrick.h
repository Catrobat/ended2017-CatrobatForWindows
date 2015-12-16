#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class NextLookBrick :
		public Brick
	{
	public:
		NextLookBrick(Script* parent);
		void Execute();
	};

}