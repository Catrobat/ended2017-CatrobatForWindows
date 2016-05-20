#pragma once

#include "Brick.h"
#include "ITurnRightBrick.h"

namespace ProjectStructure
{
	class TurnRightBrick :
		public Brick
	{
	public:
		TurnRightBrick(Catrobat_Player::NativeComponent::ITurnRightBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_rotation;
	};
}