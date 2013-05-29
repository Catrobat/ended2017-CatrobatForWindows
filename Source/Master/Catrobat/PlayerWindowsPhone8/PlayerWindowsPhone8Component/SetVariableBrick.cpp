#include "pch.h"
#include "SetVariableBrick.h"


SetVariableBrick::SetVariableBrick(string objectReference, Script *parent)
	: VariableManagementBrick(TypeOfBrick::SetVariableBrick, objectReference, parent)
{
}

void SetVariableBrick::Execute()
{
}

//void SetVariableBrick::SetVariable(UserVariable *variable)
//{
//	if (variable != NULL)
//		m_variable = variable;
//}


