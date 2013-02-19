#include "pch.h"
#include "SpriteList.h"


SpriteList::SpriteList()
{
	m_sprites = new list<Sprite*>();
}

void SpriteList::addSprite(Sprite *sprite)
{
	m_sprites->push_back(sprite);
}

int SpriteList::Size()
{
	return m_sprites->size();
}

Sprite *SpriteList::getSprite(int index)
{
	list<Sprite*>::iterator it = m_sprites->begin();
	advance(it, index);
	return *it;
}