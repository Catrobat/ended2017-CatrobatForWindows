#include "pch.h"
#include "CostumeBrick.h"

CostumeBrick::CostumeBrick(string spriteReference, string costumeDataReference, int index, Script *parent) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference, parent), m_costumeDataReference(costumeDataReference), m_index(index)
{
}

CostumeBrick::CostumeBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference, parent), m_index(0)
{
}

int CostumeBrick::Index()
{
	if (m_index > 0)
		return m_index - 1;
	return m_index;
}