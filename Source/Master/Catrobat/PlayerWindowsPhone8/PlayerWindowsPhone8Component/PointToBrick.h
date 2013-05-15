#pragma once
#include "brick.h"
class PointToBrick :
	public Brick
{
public:
	PointToBrick(string spriteReference, float rotation, Script *parent);
	void Execute();
private:
	float m_rotation;
};
