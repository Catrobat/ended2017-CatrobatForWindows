#include "pch.h"
#include "VariableManagementBrick.h"

VariableManagementBrick::VariableManagementBrick(TypeOfBrick brickType, string objectReference, FormulaTree *variableFormula, Script *parent) :
	Brick(brickType, objectReference, parent), m_variableFormula(variableFormula)
{
}

void VariableManagementBrick::SetVariable(UserVariable *variable)
{
	if (variable != NULL)
		m_variable = variable;
}
