#pragma once
#include "brick.h"
class SetSizeToBrick :
	public Brick
{
public:
	SetSizeToBrick(string spriteReference, float scale, Script *parent);
	void Execute();
private:
	float m_scale;
};
