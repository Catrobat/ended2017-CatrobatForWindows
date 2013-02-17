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
