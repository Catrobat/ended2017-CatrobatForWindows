#include "pch.h"
#include "HideBrick.h"
#include "Script.h"
#include "Object.h"

HideBrick::HideBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::HideBrick, spriteReference, parent)
{
}

void HideBrick::Execute()
{
	m_parent->Parent()->SetTransparency(1.0f);
}