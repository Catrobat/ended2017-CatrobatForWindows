#pragma once

#include "VariableManagementBrick.h"

namespace ProjectStructure
{
	class ChangeVariableBrick
		: public VariableManagementBrick
	{
	public:
		ChangeVariableBrick(FormulaTree *variableFormula, Script* parent);
		void Execute();
	};
}