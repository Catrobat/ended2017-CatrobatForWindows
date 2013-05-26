#include "pch.h"
#include "VariableManagementBrick.h"

VariableManagementBrick::VariableManagementBrick(TypeOfBrick brickType, string objectReference, Script *parent) :
	Brick(brickType, objectReference, parent)
{
}

void VariableManagementBrick::SetVariable(UserVariable *variable)
{
	if (variable != NULL)
		m_variable = variable;
}
