#pragma once

#include "Brick.h"
#include "ICostumeBrick.h"

namespace ProjectStructure
{
	class CostumeBrick :
		public Brick
	{
	public:
		CostumeBrick(Catrobat_Player::NativeComponent::ICostumeBrick^ brick, Script* parent);
		CostumeBrick(Script* parent);

		void Execute();
		int GetIndex();
	private:
		std::string m_costumeDataReference;
		int m_index;
	};
}