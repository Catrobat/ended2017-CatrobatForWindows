#pragma once

#include "variablemanagementbrick.h"

class ChangeVariableBrick
	: public VariableManagementBrick
{
public:
	ChangeVariableBrick(string objectReference, FormulaTree *variableFormula, Script *parent);
	void Execute();

};

