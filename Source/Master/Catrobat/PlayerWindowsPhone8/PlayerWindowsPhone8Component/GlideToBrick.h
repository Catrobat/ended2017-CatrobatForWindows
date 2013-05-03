#pragma once
#include "brick.h"
class GlideToBrick :
	public Brick
{
public:
	GlideToBrick(string spriteReference, float xDestination, float yDestination, float duration, Script *parent);
	void Execute();
private:
	float m_xDestination;
	float m_yDestination;
	float m_duration;
};
