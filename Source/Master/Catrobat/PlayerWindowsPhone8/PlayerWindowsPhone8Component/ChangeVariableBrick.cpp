#include "pch.h"
#include "ChangeVariableBrick.h"


ChangeVariableBrick::ChangeVariableBrick(string objectReference, FormulaTree *variableFormula, Script *parent)
	: VariableManagementBrick(TypeOfBrick::SetVariableBrick, objectReference, variableFormula, parent)
{
}

void ChangeVariableBrick::Execute()
{
}