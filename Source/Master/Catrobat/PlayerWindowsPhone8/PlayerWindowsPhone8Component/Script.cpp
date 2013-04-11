#include "pch.h"
#include "Script.h"

Script::Script(TypeOfScript scriptType, string spriteReference, Sprite *parent) :
	m_scriptType(scriptType), m_spriteReference(spriteReference), m_parent(parent)
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

Script::TypeOfScript Script::getType()
{
	return m_scriptType;
}

string Script::SpriteReference()
{
	return m_spriteReference;
}

int Script::BrickListSize()
{
	return m_brickList->size();
}

Brick *Script::GetBrick(int index)
{
	list<Brick*>::iterator it = m_brickList->begin();
	advance(it, index);
	return *it;
}

void Script::Execute()
{
	for (int i = 0; i < BrickListSize(); i++)
	{
		GetBrick(i)->Execute();
	}
}

Sprite *Script::Parent()
{
	return m_parent;
}