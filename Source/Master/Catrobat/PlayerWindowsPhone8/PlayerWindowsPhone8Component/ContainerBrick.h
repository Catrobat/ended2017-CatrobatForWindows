#pragma once

#include "Brick.h"
#include "BaseObject.h"
#include <list>

class ContainerBrick :
	public Brick
{
public:
	ContainerBrick(TypeOfBrick brickType, Script *parent);

	virtual void Execute() = 0;
	virtual void AddBrick(Brick *brick) = 0;
private:
};

