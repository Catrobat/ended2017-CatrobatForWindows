#pragma once

#include "VariableManagementBrick.h"

namespace ProjectStructure
{
	class SetVariableBrick
		: public VariableManagementBrick
	{
	public:
		SetVariableBrick(FormulaTree *variableFormula, Script* parent);
		void Execute();
	};
}