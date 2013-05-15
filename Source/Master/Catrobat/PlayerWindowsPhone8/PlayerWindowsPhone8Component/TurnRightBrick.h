#pragma once
#include "brick.h"
class TurnRightBrick :
	public Brick
{
public:
	TurnRightBrick(string spriteReference, float rotation, Script *parent);
	void Execute();
private:
	float m_rotation;
};
