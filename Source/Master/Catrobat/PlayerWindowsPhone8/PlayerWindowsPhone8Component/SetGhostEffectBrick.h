#pragma once
#include "brick.h"
#include "Script.h"
class SetGhostEffectBrick :
	public Brick
{
public:
	SetGhostEffectBrick(string spriteReference, Script* parent, float transparency);

private:
	float m_transpareny;
};

