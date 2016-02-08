#pragma once

#include "ContainerBrick.h"
#include "Object.h"
#include "IRepeatBrick.h"

#include <list>

namespace ProjectStructure
{
	class RepeatBrick :
		public ContainerBrick
	{
	public:
		RepeatBrick(Catrobat_Player::NativeComponent::IRepeatBrick^ brick, Script* parent);
		~RepeatBrick();

		void Execute();
	private:
		std::shared_ptr<FormulaTree> m_timesToRepeat;
	};
}