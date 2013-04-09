#include "pch.h"
#include "CostumeBrick.h"
#include "Script.h"

CostumeBrick::CostumeBrick(string spriteReference, string costumeDataReference, Script* parent, int index) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference, parent), m_costumeDataReference(costumeDataReference), m_index(index)
{
}

CostumeBrick::CostumeBrick(string spriteReference, Script* parent) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference, parent), m_index(0)
{
}

