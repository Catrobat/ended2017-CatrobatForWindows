#pragma once

#include "ContainerBrick.h"
#include "Object.h"
#include <list>

namespace ProjectStructure
{
	class RepeatBrick :
		public ContainerBrick
	{
	public:
		RepeatBrick(FormulaTree *times, Script* parent);
		~RepeatBrick();

		void Execute();
		void AddBrick(std::unique_ptr<Brick> brick);
	private:
		std::list<std::unique_ptr<Brick>> m_brickList;
		FormulaTree *m_timesToRepeat;
	};
}