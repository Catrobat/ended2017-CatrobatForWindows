#include "pch.h"
#include "HideBrick.h"
#include "Script.h"
#include "Sprite.h"

HideBrick::HideBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::HideBrick, spriteReference, parent)
{
}

void HideBrick::Execute()
{
	m_parent->Parent()->SetTransparency(0.0f);
}