#include "pch.h"
#include "CostumeBrick.h"

CostumeBrick::CostumeBrick(string spriteReference, string costumeDataReference, int index, Script *parent) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference, parent), m_costumeDataReference(costumeDataReference), m_index(index)
{
}

CostumeBrick::CostumeBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference, parent)
{
}