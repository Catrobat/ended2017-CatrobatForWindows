#pragma once

#include "Brick.h"
#include "IChangeXByBrick.h"

namespace ProjectStructure
{
	class ChangeXByBrick :
		public Brick
	{
	public:
		ChangeXByBrick(Catrobat_Player::NativeComponent::IChangeXByBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_offsetX;
	};

}