#pragma once
#include "brick.h"
class SetXBrick :
	public Brick
{
public:
	SetXBrick(string spriteReference, float positionX, Script *parent);
	void Execute();
private:
	float m_positionX;
};
