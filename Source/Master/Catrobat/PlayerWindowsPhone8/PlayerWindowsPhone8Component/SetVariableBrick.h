#pragma once
#include "VariableManagementBrick.h"

class SetVariableBrick
	: public VariableManagementBrick
{
public:
	SetVariableBrick(FormulaTree *variableFormula, Script *parent);
	void Execute();
};

