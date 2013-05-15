#pragma once
#include "brick.h"
class SetYBrick :
	public Brick
{
public:
	SetYBrick(string spriteReference, float positionY, Script *parent);
	void Execute();
private:
	float m_positionY;
};
