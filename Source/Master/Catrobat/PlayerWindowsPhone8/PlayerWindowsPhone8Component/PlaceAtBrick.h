#pragma once
#include "brick.h"
#include "Script.h"
class PlaceAtBrick :
	public Brick
{
public:
	PlaceAtBrick(string spriteReference, Script* parent, float positionX, float positionY);

private:
	float m_positionX;
	float m_positionY;
};

