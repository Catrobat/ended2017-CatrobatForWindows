#pragma once

#include "ContainerBrick.h"
#include "Object.h"
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
	std::list<Brick*> *m_ifList;
	std::list<Brick*> *m_elseList;
	FormulaTree *m_condition;
	IfBranchType m_currentAddMode;

	Brick *GetIfBrick(int index);
	Brick *GetElseBrick(int index);
};

