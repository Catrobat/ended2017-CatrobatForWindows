#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class HideBrick :
		public Brick
	{
	public:
		HideBrick(Script* parent);
		void Execute();
	};
}