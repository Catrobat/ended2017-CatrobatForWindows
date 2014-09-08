#pragma once

#include "Brick.h"

class CostumeBrick :
	public Brick
{
public:
	CostumeBrick(std::string costumeDataReference, int index, Script *parent);
	CostumeBrick(Script *parent);

	void Execute();
	int GetIndex();
private:
	std::string m_costumeDataReference;
	int m_index;
};
