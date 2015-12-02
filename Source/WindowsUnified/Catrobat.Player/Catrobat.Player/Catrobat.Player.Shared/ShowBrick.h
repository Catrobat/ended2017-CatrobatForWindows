#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class ShowBrick :
		public Brick
	{
	public:
		ShowBrick(std::shared_ptr<Script> parent);
		void Execute();
	};
}