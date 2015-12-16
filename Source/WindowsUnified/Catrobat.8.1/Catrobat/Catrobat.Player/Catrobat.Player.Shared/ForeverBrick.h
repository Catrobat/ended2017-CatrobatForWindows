#pragma once

#include "ContainerBrick.h"
#include "Object.h"
#include <list>

namespace ProjectStructure
{
	class ForeverBrick :
		public ContainerBrick
	{
	public:
		ForeverBrick(Script* parent);
		~ForeverBrick();

		void Execute();
		void AddBrick(std::unique_ptr<Brick> brick);
	private:
		std::list<std::unique_ptr<Brick>> m_brickList;
	};
}
