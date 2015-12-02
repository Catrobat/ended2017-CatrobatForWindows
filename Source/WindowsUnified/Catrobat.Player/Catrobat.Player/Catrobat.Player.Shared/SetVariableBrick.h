#pragma once

#include "VariableManagementBrick.h"

namespace ProjectStructure
{
	class SetVariableBrick
		: public VariableManagementBrick
	{
	public:
		SetVariableBrick(FormulaTree *variableFormula, std::shared_ptr<Script> parent);
		void Execute();
	};
}