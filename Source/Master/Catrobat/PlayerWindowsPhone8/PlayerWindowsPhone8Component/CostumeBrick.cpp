#include "pch.h"
#include "CostumeBrick.h"
#include "Script.h"
#include "Object.h"

CostumeBrick::CostumeBrick(string spriteReference, string costumeDataReference, int index, Script *parent) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference, parent), m_costumeDataReference(costumeDataReference), m_index(index)
{
}

CostumeBrick::CostumeBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::CostumeBrick, spriteReference, parent), m_index(0)
{
}

int CostumeBrick::GetIndex()
{
	if (m_index > 0)
		return m_index - 1;
	return m_index;
}

void CostumeBrick::Execute()
{	
	m_parent->GetParent()->SetLook(m_index);
}