#include "pch.h"
#include "NextLookBrick.h"
#include "Script.h"
#include "Object.h"

NextLookBrick::NextLookBrick(Script *parent) :
	Brick(TypeOfBrick::NextlookBrick, parent)
{
}

void NextLookBrick::Execute()
{	
	int next = m_parent->GetParent()->GetLook() + 1;
	if (next >= m_parent->GetParent()->GetLookCount())
	{
		next = 0;
	}

	m_parent->GetParent()->SetLook(next);
}