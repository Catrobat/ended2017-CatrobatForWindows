#pragma once
#include "brick.h"
class ChangeSizeByBrick :
	public Brick
{
public:
	ChangeSizeByBrick(string spriteReference, float scale, Script *parent);
	void Execute();
private:
	float m_scale;
};
