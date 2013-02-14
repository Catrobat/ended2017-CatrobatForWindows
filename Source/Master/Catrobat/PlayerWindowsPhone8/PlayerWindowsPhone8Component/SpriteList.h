#pragma once

#include <list>

#include "Sprite.h"

using namespace std;

class SpriteList
{
public:
	SpriteList();
	~SpriteList();

	void addSprite(Sprite *sprite);

private:
	list<Sprite*> *m_sprites;
};

