#pragma once
#include "brick.h"
class SetGhostEffectBrick :
	public Brick
{
public:
	SetGhostEffectBrick(string spriteReference, float transparency, Script *parent);
	void Execute();
private:
	float m_transpareny;
};
