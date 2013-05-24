#include "pch.h"
#include "IfBrick.h"
#include "Interpreter.h"

IfBrick::IfBrick(string spriteReference, FormulaTree *condition, Script *parent) :
	Brick(TypeOfBrick::IfBrick, spriteReference, parent), m_condition(condition)
{
	m_ifList = new list<Brick*>();
	m_elseList = new list<Brick*>();

	currentAddMode = IfBranchType::If;
}


IfBrick::~IfBrick(void)
{
	delete m_ifList;
	delete m_elseList;

	// TODO: Delete Condition?
}

void IfBrick::Execute()
{
	// Synchronously execute all subsequent blocks
	if (Interpreter::Instance()->EvaluateFormulaToBool(m_condition))
	{
		for (int i = 0; i < m_ifList->size(); i++)
		{
			GetIfBrick(i)->Execute();
		}
	}
	else
	{
		for (int i = 0; i < m_elseList->size(); i++)
		{
			GetElseBrick(i)->Execute();
		}
	}
}

void IfBrick::addBrick(Brick *brick)
{
	if (currentAddMode == IfBranchType::If)
	{
		addIfBrick(brick);
	}
	else
	{
		addElseBrick(brick);
	}
}

void IfBrick::setCurrentAddMode(IfBranchType mode)
{
	currentAddMode = mode;
}

void IfBrick::addIfBrick(Brick *brick)
{
	m_ifList->push_back(brick);
}

void IfBrick::addElseBrick(Brick *brick)
{
	m_elseList->push_back(brick);
}

Brick *IfBrick::GetIfBrick(int index)
{
	list<Brick*>::iterator it = m_ifList->begin();
	advance(it, index);
	return *it;
}

Brick *IfBrick::GetElseBrick(int index)
{
	list<Brick*>::iterator it = m_elseList->begin();
	advance(it, index);
	return *it;
}