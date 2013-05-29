#include "pch.h"
#include "ChangeVariableBrick.h"


ChangeVariableBrick::ChangeVariableBrick(string objectReference, Script *parent)
	: VariableManagementBrick(TypeOfBrick::SetVariableBrick, objectReference, parent)
{
}

void ChangeVariableBrick::Execute()
{
}