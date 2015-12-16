#include "pch.h"
#include "CostumeBrick.h"
#include "Script.h"
#include "Object.h"

using namespace ProjectStructure;

CostumeBrick::CostumeBrick(std::string costumeDataReference, int index, Script* parent) :
	Brick(TypeOfBrick::CostumeBrick, parent), m_costumeDataReference(costumeDataReference), m_index(index)
{
}

CostumeBrick::CostumeBrick(Script* parent) :
	Brick(TypeOfBrick::CostumeBrick, parent), m_index(0)
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