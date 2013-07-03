#include "pch.h"
#include "IfBrick.h"
#include "Interpreter.h"

IfBrick::IfBrick(string spriteReference, FormulaTree *condition, Script *parent) :
	ContainerBrick(TypeOfBrick::IfBrick, spriteReference, parent), m_condition(condition)
{
	m_ifList = new list<Brick*>();
	m_elseList = new list<Brick*>();

	m_currentAddMode = IfBranchType::If;
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
	if (Interpreter::Instance()->EvaluateFormulaToBool(m_condition, GetParent()->GetParent()))
	{
		for (unsigned int i = 0; i < m_ifList->size(); i++)
		{
			GetIfBrick(i)->Execute();
		}
	}
	else
	{
		for (unsigned int i = 0; i < m_elseList->size(); i++)
		{
			GetElseBrick(i)->Execute();
		}
	}
}

void IfBrick::AddBrick(Brick *brick)
{
	if (m_currentAddMode == IfBranchType::If)
	{
		AddIfBrick(brick);
	}
	else
	{
		AddElseBrick(brick);
	}
}

void IfBrick::SetCurrentAddMode(IfBranchType mode)
{
	m_currentAddMode = mode;
}

void IfBrick::AddIfBrick(Brick *brick)
{
	m_ifList->push_back(brick);
}

void IfBrick::AddElseBrick(Brick *brick)
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