#include "pch.h"
#include "VariableManagementBrick.h"

using namespace std;
using namespace ProjectStructure;

VariableManagementBrick::VariableManagementBrick(TypeOfBrick brickType, FormulaTree *variableFormula, Script* parent) :
	Brick(brickType, parent), m_variableFormula(variableFormula)
{
}

void VariableManagementBrick::SetVariable(shared_ptr<UserVariable> variable)
{
	if (variable != NULL)
	{
		m_variable = variable;
	}
}
