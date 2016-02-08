#pragma once

#include "Brick.h"
#include "Object.h"
#include "IContainerBrick.h"

#include <list>

namespace ProjectStructure
{
	class ContainerBrick :
		public Brick
	{
	public:
		ContainerBrick(TypeOfBrick brickType, Catrobat_Player::NativeComponent::IContainerBrick^ brick, Script* parent);

		virtual void Execute() = 0;
	protected:
		std::list<std::unique_ptr<Brick>> m_brickList;
	};
}