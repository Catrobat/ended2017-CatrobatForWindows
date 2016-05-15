#pragma once

#include "Brick.h"
#include "IPlaceAtBrick.h"

namespace ProjectStructure
{
	class PlaceAtBrick :
		public Brick
	{
	public:
		PlaceAtBrick(Catrobat_Player::NativeComponent::IPlaceAtBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_positionX;
		std::shared_ptr<FormulaTree> m_positionY;
	};
}