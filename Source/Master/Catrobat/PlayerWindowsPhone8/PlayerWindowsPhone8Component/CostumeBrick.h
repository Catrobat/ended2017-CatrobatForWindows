#pragma once
#include "brick.h"
class CostumeBrick :
	public Brick
{
public:
	CostumeBrick(string spriteReference, string costumeDataReference);

	string m_costumeDataReference;
};

