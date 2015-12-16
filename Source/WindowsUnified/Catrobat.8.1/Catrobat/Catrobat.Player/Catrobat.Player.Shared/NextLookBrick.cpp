#include "pch.h"
#include "NextLookBrick.h"
#include "Script.h"
#include "Object.h"

using namespace ProjectStructure;

NextLookBrick::NextLookBrick(Script* parent) :
	Brick(TypeOfBrick::NextlookBrick, parent)
{
}

void NextLookBrick::Execute()
{
	int next = m_parent->GetParent()->GetIndexOfCurrentLook() + 1;
	if (next >= m_parent->GetParent()->GetLookListSize())
	{
		next = 0;
	}

	m_parent->GetParent()->SetLook(next);
}