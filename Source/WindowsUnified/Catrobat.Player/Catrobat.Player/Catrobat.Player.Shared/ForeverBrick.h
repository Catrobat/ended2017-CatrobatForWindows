#pragma once

#include "ContainerBrick.h"
#include "Object.h"
#include <list>

class ForeverBrick :
	public ContainerBrick
{
public:
	ForeverBrick(std::shared_ptr<Script> parent);
	~ForeverBrick();

	void Execute();
	void AddBrick(std::unique_ptr<Brick> brick);
private:
	std::list<std::unique_ptr<Brick>> m_brickList;
};

