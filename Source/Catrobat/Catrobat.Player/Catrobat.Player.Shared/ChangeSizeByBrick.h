#pragma once

#include "Brick.h"
#include "FormulaTree.h"
#include "IChangeSizeByBrick.h"

namespace ProjectStructure
{
	class ChangeSizeByBrick :
		public Brick
	{
	public:
		ChangeSizeByBrick(Catrobat_Player::NativeComponent::IChangeSizeByBrick^ brick, Script* parent);
		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_scale;
	};

}