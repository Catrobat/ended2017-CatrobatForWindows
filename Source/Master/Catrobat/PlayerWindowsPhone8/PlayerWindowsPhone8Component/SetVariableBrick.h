#pragma once
#include "VariableManagementBrick.h"

class SetVariableBrick
	: public VariableManagementBrick
{
public:
	SetVariableBrick(string objectReference, FormulaTree *variableFormula, Script *parent);
	void Execute();
};

