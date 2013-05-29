#pragma once

#include "variablemanagementbrick.h"

class ChangeVariableBrick
	: public VariableManagementBrick
{
public:
	ChangeVariableBrick(string objectReference, Script *parent);
	void Execute();

};

