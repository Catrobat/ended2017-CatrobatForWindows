#pragma once

#include "ContainerBrick.h"
#include "Object.h"
#include <list>

namespace ProjectStructure
{
	enum IfBranchType
	{
		If,
		Else
	};

	class IfBrick :
		public ContainerBrick
	{
	public:
		IfBrick(FormulaTree *condition, std::shared_ptr<Script> parent);
		~IfBrick();

		void Execute();
		void AddBrick(std::unique_ptr<Brick> brick);
		void SetCurrentAddMode(IfBranchType mode);
	private:
		std::list<std::unique_ptr<Brick>> m_ifList;
		std::list<std::unique_ptr<Brick>> m_elseList;
		FormulaTree *m_condition;
		IfBranchType m_currentAddMode;
	};
}