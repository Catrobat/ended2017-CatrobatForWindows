#pragma once
#include "brick.h"
class ChangeYByBrick :
	public Brick
{
public:
	ChangeYByBrick(string spriteReference, float offsetY, Script *parent);
	void Execute();
private:
	float m_offsetY;
};
