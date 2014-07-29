#pragma once

#include "ContainerBrick.h"
#include "BaseObject.h"
#include <list>

class RepeatBrick :
	public ContainerBrick
{
public:
	RepeatBrick(FormulaTree *times, Script *parent);
	~RepeatBrick(void);

	void Execute();
	void AddBrick(Brick *brick);
private:
	list<Brick*> *m_brickList;

	Brick *GetBrick(int index);
	FormulaTree *m_timesToRepeat;
};

