#pragma once
#include "brick.h"
class CostumeBrick :
	public Brick
{
public:
	CostumeBrick(string spriteReference, string costumeDataReference, int index, Script *parent);
	CostumeBrick(string spriteReference, Script *parent);

private:
	string m_costumeDataReference;
	int m_index;
};
