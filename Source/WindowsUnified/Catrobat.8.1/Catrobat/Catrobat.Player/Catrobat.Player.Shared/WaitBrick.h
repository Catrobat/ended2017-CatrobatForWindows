#pragma once

#include "Brick.h"
#include "FormulaTree.h"
#include "IWaitBrick.h"

namespace ProjectStructure
{
	class WaitBrick :
		public Brick
	{
	public:
		WaitBrick(Catrobat_Player::NativeComponent::IWaitBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_timeToWaitInSeconds;
	};
}