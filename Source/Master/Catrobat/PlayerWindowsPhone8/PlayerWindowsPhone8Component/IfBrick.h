#pragma once

#include "Brick.h"
#include "BaseObject.h"
#include <list>

class IfBrick :
	public Brick
{
public:
	IfBrick(string spriteReference, FormulaTree *condition, Script *parent);
	~IfBrick(void);

	void Execute();
	void addIfBrick(Brick *brick);
	void addElseBrick(Brick *brick);
private:
	list<Brick*> *m_ifList;
	list<Brick*> *m_elseList;
	FormulaTree *m_condition;

	Brick *GetIfBrick(int index);
	Brick *GetElseBrick(int index);
};

