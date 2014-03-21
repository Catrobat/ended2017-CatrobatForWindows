#pragma once
#include "VariableManagementBrick.h"

class ChangeVariableBrick
	: public VariableManagementBrick
{
public:
	ChangeVariableBrick(FormulaTree *variableFormula, Script *parent);
	void Execute();
};

