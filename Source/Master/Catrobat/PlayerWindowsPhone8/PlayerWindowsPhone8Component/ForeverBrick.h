#pragma once

#include "ContainerBrick.h"
#include "BaseObject.h"
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
	list<Brick*> *m_brickList;

	Brick *GetBrick(int index);
};

