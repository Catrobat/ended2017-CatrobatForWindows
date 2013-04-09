#pragma once
#include "brick.h"
class CostumeBrick :
	public Brick
{
public:
	CostumeBrick(string spriteReference, string costumeDataReference, int index);
	CostumeBrick(string spriteReference);

	private:
	string m_costumeDataReference;
	int m_index;
};

