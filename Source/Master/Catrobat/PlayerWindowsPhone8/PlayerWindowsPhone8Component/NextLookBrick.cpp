#include "pch.h"
#include "NextLookBrick.h"
#include "Script.h"
#include "Object.h"

NextLookBrick::NextLookBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::NextlookBrick, spriteReference, parent)
{
}

void NextLookBrick::Execute()
{	
	int next = m_parent->Parent()->GetLook() + 1;
	if (next > m_parent->Parent()->GetLookCount())
	{
		next = 0;
	}

	m_parent->Parent()->SetLook(next);
}