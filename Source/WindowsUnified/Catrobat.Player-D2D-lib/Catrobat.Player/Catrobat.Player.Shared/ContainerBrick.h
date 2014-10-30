#pragma once

#include "Brick.h"
#include "Object.h"
#include <list>

class ContainerBrick :
	public Brick
{
public:
	ContainerBrick(TypeOfBrick brickType, std::shared_ptr<Script> parent);

	virtual void Execute() = 0;
	virtual void AddBrick(Brick *brick) = 0;
private:
};

