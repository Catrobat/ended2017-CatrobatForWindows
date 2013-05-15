#pragma once
#include "brick.h"
class ChangeXByBrick :
	public Brick
{
public:
	ChangeXByBrick(string spriteReference, float offsetX, Script *parent);
	void Execute();
private:
	float m_offsetX;
};
