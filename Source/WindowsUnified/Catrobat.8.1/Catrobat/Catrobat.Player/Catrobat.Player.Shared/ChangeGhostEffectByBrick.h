#pragma once

#include "Brick.h"
#include "IChangeGhostEffectByBrick.h"

namespace ProjectStructure
{
	class ChangeGhostEffectByBrick :
		public Brick
	{
	public:
		ChangeGhostEffectByBrick(Catrobat_Player::NativeComponent::IChangeGhostEffectByBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_transparency;
	};
}