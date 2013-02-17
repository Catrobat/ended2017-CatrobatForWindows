#include "pch.h"
#include "CostumeBrick.h"


CostumeBrick::CostumeBrick(string spriteReference, string costumeDataReference) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference), m_costumeDataReference(costumeDataReference)
{
}

