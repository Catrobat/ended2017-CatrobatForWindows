#pragma once

#include "Brick.h"
#include "ISetXBrick.h"

namespace ProjectStructure
{
	class SetXBrick :
		public Brick
	{
	public:
		SetXBrick(Catrobat_Player::NativeComponent::ISetXBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_positionX;
	};
}