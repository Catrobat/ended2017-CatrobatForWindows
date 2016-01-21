#pragma once

#include "Brick.h"
#include "ITurnLeftBrick.h"

namespace ProjectStructure
{
	class TurnLeftBrick :
		public Brick
	{
	public:
		TurnLeftBrick(Catrobat_Player::NativeComponent::ITurnLeftBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_rotation;
	};
}