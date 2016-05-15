#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class ShowBrick :
		public Brick
	{
	public:
		ShowBrick(Script* parent);
		void Execute();
	};
}