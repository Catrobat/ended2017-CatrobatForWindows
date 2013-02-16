#pragma once
#include "brick.h"
class SetGhostEffectBrick :
	public Brick
{
public:
	SetGhostEffectBrick(string spriteReference, float transparency);

private:
	float m_transpareny;
};

