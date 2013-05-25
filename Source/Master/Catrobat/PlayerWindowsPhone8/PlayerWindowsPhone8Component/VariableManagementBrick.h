#pragma once
#include "brick.h"

class VariableManagementBrick :
	public Brick
{
public:
	VariableManagementBrick(TypeOfBrick brickType, string objectReference, Script *parent);

	virtual void Execute() = 0;
};
