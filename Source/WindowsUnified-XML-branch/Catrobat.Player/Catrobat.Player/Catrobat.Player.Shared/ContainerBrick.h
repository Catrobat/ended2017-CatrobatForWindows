#pragma once

#include "Brick.h"
#include "Object.h"
#include <list>

namespace ProjectStructure
{
	class ContainerBrick :
		public Brick
	{
	public:
		ContainerBrick(TypeOfBrick brickType, Script* parent);

		virtual void Execute() = 0;
		virtual void AddBrick(std::unique_ptr<Brick> brick) = 0;
	private:
	};
}