#pragma once

#include "Brick.h"
#include "ISetYBrick.h"

namespace ProjectStructure
{
	class SetYBrick :
		public Brick
	{
	public:
		SetYBrick(Catrobat_Player::NativeComponent::ISetYBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_positionY;
	};
}