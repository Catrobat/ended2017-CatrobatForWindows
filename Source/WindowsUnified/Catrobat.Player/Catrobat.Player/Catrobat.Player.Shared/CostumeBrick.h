#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class CostumeBrick :
		public Brick
	{
	public:
		CostumeBrick(std::string costumeDataReference, int index, std::shared_ptr<Script> parent);
		CostumeBrick(std::shared_ptr<Script> parent);

		void Execute();
		int GetIndex();
	private:
		std::string m_costumeDataReference;
		int m_index;
	};
}