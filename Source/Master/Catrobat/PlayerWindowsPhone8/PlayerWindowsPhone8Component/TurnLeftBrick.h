#pragma once
#include "brick.h"
class TurnLeftBrick :
	public Brick
{
public:
	TurnLeftBrick(string spriteReference, float rotation, Script *parent);
	void Execute();
private:
	float m_rotation;
};
