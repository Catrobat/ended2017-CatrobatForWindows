#include "pch.h"
#include "CostumeBrick.h"


CostumeBrick::CostumeBrick(string spriteReference, string costumeDataReference, int index) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference), m_costumeDataReference(costumeDataReference), m_index(index)
{
}

CostumeBrick::CostumeBrick(string spriteReference) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference)
{
}

