#include "pch.h"
#include "VariableManagementBrick.h"

VariableManagementBrick::VariableManagementBrick(TypeOfBrick brickType, FormulaTree *variableFormula, std::shared_ptr<Script> parent) :
	Brick(brickType, parent), m_variableFormula(variableFormula)
{
}

void VariableManagementBrick::SetVariable(UserVariable *variable)
{
    if (variable != NULL)
    {
        m_variable = variable;
    }
}
