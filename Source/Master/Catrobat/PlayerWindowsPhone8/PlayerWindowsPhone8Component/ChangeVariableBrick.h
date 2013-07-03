#pragma once
#include "VariableManagementBrick.h"

class ChangeVariableBrick
	: public VariableManagementBrick
{
public:
	ChangeVariableBrick(string objectReference, FormulaTree *variableFormula, Script *parent);
	void Execute();
};

