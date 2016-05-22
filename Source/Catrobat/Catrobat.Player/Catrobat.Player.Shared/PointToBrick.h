#pragma once

#include "Brick.h"
#include "IPointToBrick.h"

namespace ProjectStructure
{
	class PointToBrick :
		public Brick
	{
	public:
		PointToBrick(Catrobat_Player::NativeComponent::IPointToBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_rotation;
	};
}