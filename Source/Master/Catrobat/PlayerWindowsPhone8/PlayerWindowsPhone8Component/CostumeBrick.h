#pragma once
#include "Brick.h"
class CostumeBrick :
	public Brick
{
public:
	CostumeBrick(string costumeDataReference, int index, Script *parent);
	CostumeBrick(Script *parent);

	void Execute();
	int GetIndex();
private:
	string m_costumeDataReference;
	int m_index;
};
