#include "pch.h"
#include "CostumeBrick.h"
#include "Script.h"
#include "Object.h"
#include "Helper.h"

using namespace ProjectStructure;

CostumeBrick::CostumeBrick(Catrobat_Player::NativeComponent::ICostumeBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::CostumeBrick, parent),
	m_costumeDataReference(Helper::StdString(brick->CostumeDataReference)),
	m_index(brick->Index)
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