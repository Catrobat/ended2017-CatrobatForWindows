#pragma once

#include "Brick.h"
#include "IGlideBrick.h"

namespace ProjectStructure
{
	class GlideToBrick :
		public Brick
	{
	public:
		GlideToBrick(Catrobat_Player::NativeComponent::IGLideToBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_xDestination;
		std::shared_ptr<FormulaTree> m_yDestination;
		std::shared_ptr<FormulaTree> m_duration;
	};
}