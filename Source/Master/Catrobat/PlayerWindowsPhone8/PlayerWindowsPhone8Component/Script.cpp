#include "pch.h"
#include "Script.h"


Script::Script(TypeOfScript scriptType) :
	m_scriptType(scriptType)
{
	m_brickList = new list<Brick*>();
}

void Script::addBrick(Brick *brick)
{
	m_brickList->push_back(brick);
}