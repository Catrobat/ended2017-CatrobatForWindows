#pragma once

#include "VariableManagementBrick.h"

namespace ProjectStructure
{
	class ChangeVariableBrick
		: public VariableManagementBrick
	{
	public:
		ChangeVariableBrick(FormulaTree *variableFormula, std::shared_ptr<Script> parent);
		void Execute();
	};
}