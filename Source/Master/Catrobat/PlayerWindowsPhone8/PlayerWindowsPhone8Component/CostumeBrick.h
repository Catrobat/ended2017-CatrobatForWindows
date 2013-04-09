#pragma once
#include "brick.h"
class CostumeBrick :
	public Brick
{
public:
	CostumeBrick(string spriteReference, string costumeDataReference, Script* parent, int index);
	CostumeBrick(string spriteReference, Script* parent);

private:
	string m_costumeDataReference;
	int m_index;
};

