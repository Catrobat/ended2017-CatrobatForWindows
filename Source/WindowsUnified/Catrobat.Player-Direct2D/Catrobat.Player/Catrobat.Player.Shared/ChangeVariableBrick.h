#pragma once

#include "VariableManagementBrick.h"

class ChangeVariableBrick
	: public VariableManagementBrick
{
public:
	ChangeVariableBrick(FormulaTree *variableFormula, std::shared_ptr<Script> parent);
	void Execute();
};

