#pragma once

#include "ContainerBrick.h"
#include "BaseObject.h"
#include <list>

enum IfBranchType
{
	If,
	Else
};

class IfBrick :
	public ContainerBrick
{
public:
	IfBrick(string spriteReference, FormulaTree *condition, Script *parent);
	~IfBrick(void);

	void Execute();
	void addIfBrick(Brick *brick);
	void addElseBrick(Brick *brick);
	void addBrick(Brick *brick);
	void setCurrentAddMode(IfBranchType mode);
private:
	list<Brick*> *m_ifList;
	list<Brick*> *m_elseList;
	FormulaTree *m_condition;
	IfBranchType currentAddMode;

	Brick *GetIfBrick(int index);
	Brick *GetElseBrick(int index);
};

