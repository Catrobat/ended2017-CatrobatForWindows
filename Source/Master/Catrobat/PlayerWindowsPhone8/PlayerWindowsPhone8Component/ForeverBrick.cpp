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
	for (int i = 0; i < m_brickList->size(); i++)
	{
		GetBrick(i)->Execute();
	}
}

void ForeverBrick::addBrick(Brick *brick)
{
	m_brickList->push_back(brick);
}

Brick *ForeverBrick::GetBrick(int index)
{
	list<Brick*>::iterator it = m_brickList->begin();
	advance(it, index);
	return *it;
}