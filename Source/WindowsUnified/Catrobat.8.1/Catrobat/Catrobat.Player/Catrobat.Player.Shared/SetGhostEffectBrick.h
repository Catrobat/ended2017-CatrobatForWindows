#pragma once

#include "Brick.h"
#include "ISetGhostEffectBrick.h"

namespace ProjectStructure
{
	class SetGhostEffectBrick :
		public Brick
	{
	public:
		SetGhostEffectBrick(Catrobat_Player::NativeComponent::ISetGhostEffectBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_transparency;
	};
}