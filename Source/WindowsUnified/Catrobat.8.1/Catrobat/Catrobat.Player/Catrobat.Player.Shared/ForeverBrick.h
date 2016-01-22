#pragma once

#include "ContainerBrick.h"
#include "Object.h"
#include "IForeverBrick.h"

#include <list>

namespace ProjectStructure
{
	class ForeverBrick :
		public ContainerBrick
	{
	public:
		ForeverBrick(Catrobat_Player::NativeComponent::IForeverBrick^ brick, Script* parent);
		~ForeverBrick();

		void Execute();
	private:
	};
}
