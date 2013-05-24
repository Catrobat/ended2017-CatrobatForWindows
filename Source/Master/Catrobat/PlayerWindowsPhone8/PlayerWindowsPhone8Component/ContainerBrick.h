#pragma once

#include "Brick.h"
#include "BaseObject.h"
#include <list>

class ContainerBrick :
	public Brick
{
public:
	ContainerBrick(TypeOfBrick brickType, string objectReference, Script *parent);

	virtual void Execute() = 0;
	virtual void addBrick(Brick *brick) = 0;
private:
};

