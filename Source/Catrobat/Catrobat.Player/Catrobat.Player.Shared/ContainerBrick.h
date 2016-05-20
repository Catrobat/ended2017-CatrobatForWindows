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

		virtual std::list<std::unique_ptr<Brick>> *ListPointer();
	protected:
		std::list<std::unique_ptr<Brick>> m_brickList;
	};
}