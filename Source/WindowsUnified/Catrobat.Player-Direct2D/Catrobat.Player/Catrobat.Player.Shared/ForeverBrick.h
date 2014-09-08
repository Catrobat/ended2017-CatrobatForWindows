#pragma once

#include "ContainerBrick.h"
#include "Object.h"
#include <list>

class ForeverBrick :
	public ContainerBrick
{
public:
	ForeverBrick(Script *parent);
	~ForeverBrick(void);

	void Execute();
	void AddBrick(Brick *brick);
private:
	std::list<Brick*> *m_brickList;

	Brick *GetBrick(int index);
};

