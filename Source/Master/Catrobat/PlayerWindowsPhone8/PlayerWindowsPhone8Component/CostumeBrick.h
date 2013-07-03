#pragma once
#include "Brick.h"
class CostumeBrick :
	public Brick
{
public:
	CostumeBrick(string spriteReference, string costumeDataReference, int index, Script *parent);
	CostumeBrick(string spriteReference, Script *parent);

	void Execute();
	int GetIndex();
private:
	string m_costumeDataReference;
	int m_index;
};
