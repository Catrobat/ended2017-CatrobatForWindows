#pragma once

#include "Brick.h"
#include "ISetSizeToBrick.h"

namespace ProjectStructure
{
	class SetSizeToBrick :
		public Brick
	{
	public:
		SetSizeToBrick(Catrobat_Player::NativeComponent::ISetSizeToBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_scale;
	};
}