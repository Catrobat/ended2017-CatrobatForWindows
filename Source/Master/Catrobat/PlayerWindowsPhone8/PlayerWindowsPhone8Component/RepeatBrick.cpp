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
	int i = 0;
	int global = 0;
	int times = Interpreter::Instance()->EvaluateFormulaToInt(m_timesToRepeat);
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

void RepeatBrick::addBrick(Brick *brick)
{
	m_brickList->push_back(brick);
}

Brick *RepeatBrick::GetBrick(int index)
{
	list<Brick*>::iterator it = m_brickList->begin();
	advance(it, index);
	return *it;
}