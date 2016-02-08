#pragma once

#include "Brick.h"
#include "IChangeYByBrick.h"

namespace ProjectStructure
{
	class ChangeYByBrick :
		public Brick
	{
	public:
		ChangeYByBrick(Catrobat_Player::NativeComponent::IChangeYByBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_offsetY;
	};
}