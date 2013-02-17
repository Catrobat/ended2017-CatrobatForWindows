#include "pch.h"
#include "Script.h"


Script::Script(TypeOfScript scriptType, string spriteReference) :
	m_scriptType(scriptType), m_spriteReference(spriteReference)
{
	m_brickList = new list<Brick*>();
}

void Script::addBrick(Brick *brick)
{
	m_brickList->push_back(brick);
}

void Script::addSpriteReference(string spriteReference)
{
	m_spriteReference = spriteReference;
}