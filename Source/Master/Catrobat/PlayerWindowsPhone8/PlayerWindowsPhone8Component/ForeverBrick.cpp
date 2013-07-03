#include "pch.h"
#include "ForeverBrick.h"
#include "Interpreter.h"

ForeverBrick::ForeverBrick(string spriteReference, Script *parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, spriteReference, parent)
{
	m_brickList = new list<Brick*>();
}


ForeverBrick::~ForeverBrick(void)
{
	delete m_brickList;
}

void ForeverBrick::Execute()
{
	// Synchronously execute all subsequent blocks
	int i = 0;
	while (true)
	{
		GetBrick(i)->Execute();
		i++;
		if (i >= m_brickList->size())
		{
			i = 0; // Reset counter
		}
	}
}

void ForeverBrick::AddBrick(Brick *brick)
{
	m_brickList->push_back(brick);
}

Brick *ForeverBrick::GetBrick(int index)
{
	list<Brick*>::iterator it = m_brickList->begin();
	advance(it, index);
	return *it;
}