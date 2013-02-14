#include "pch.h"
#include "Sprite.h"


Sprite::Sprite(string name) :
	m_name(name)
{
	m_lookDatas = new list<LookData*>();
}

void Sprite::addLookData(LookData *lookData)
{
	m_lookDatas->push_back(lookData);
}

