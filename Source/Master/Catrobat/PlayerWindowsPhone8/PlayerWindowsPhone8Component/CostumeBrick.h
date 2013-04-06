#pragma once
#include "brick.h"
class CostumeBrick :
	public Brick
{
public:
	CostumeBrick(string spriteReference, string costumeDataReference);
	CostumeBrick(string spriteReference);

	string m_costumeDataReference;
};

