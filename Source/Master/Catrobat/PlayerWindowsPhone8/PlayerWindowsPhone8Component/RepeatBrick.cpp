#include "pch.h"
#include "RepeatBrick.h"
#include "Interpreter.h"

RepeatBrick::RepeatBrick(string spriteReference, FormulaTree *times, Script *parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, spriteReference, parent), m_timesToRepeat(times)
{
	m_brickList = new list<Brick*>();
}


RepeatBrick::~RepeatBrick(void)
{
	delete m_brickList;
}

void RepeatBrick::Execute()
{
	// Synchronously execute all subsequent blocks
	unsigned int i = 0;
	int global = 0;
	int times = Interpreter::Instance()->EvaluateFormulaToInt(m_timesToRepeat, Parent()->Parent());
	while (global < times)
	{
		GetBrick(i)->Execute();
		i++;
		if (i >= m_brickList->size())
		{
			i = 0; // Reset counter
			global++;
		}
	}
}

void RepeatBrick::AddBrick(Brick *brick)
{
	m_brickList->push_back(brick);
}

Brick *RepeatBrick::GetBrick(int index)
{
	list<Brick*>::iterator it = m_brickList->begin();
	advance(it, index);
	return *it;
}