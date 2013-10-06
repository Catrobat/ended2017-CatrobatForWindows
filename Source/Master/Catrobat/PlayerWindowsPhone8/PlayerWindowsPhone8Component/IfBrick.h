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
	IfBrick(FormulaTree *condition, Script *parent);
	~IfBrick(void);

	void Execute();
	void AddIfBrick(Brick *brick);
	void AddElseBrick(Brick *brick);
	void AddBrick(Brick *brick);
	void SetCurrentAddMode(IfBranchType mode);
private:
	list<Brick*> *m_ifList;
	list<Brick*> *m_elseList;
	FormulaTree *m_condition;
	IfBranchType m_currentAddMode;

	Brick *GetIfBrick(int index);
	Brick *GetElseBrick(int index);
};

