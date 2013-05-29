#pragma once

#include "VariableManagementBrick.h"

class SetVariableBrick
	: public VariableManagementBrick
{
public:
	SetVariableBrick(string objectReference, Script *parent);
	void Execute();
};

