#pragma once

#include "ContainerBrick.h"
#include "BaseObject.h"
#include <list>

class RepeatBrick :
	public ContainerBrick
{
public:
	RepeatBrick(string spriteReference, FormulaTree *times, Script *parent);
	~RepeatBrick(void);

	void Execute();
	void addBrick(Brick *brick);
private:
	list<Brick*> *m_brickList;

	Brick *GetBrick(int index);
	FormulaTree *m_timesToRepeat;
};

