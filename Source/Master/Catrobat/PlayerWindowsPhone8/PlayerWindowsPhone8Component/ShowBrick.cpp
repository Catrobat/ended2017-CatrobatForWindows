#include "pch.h"
#include "ShowBrick.h"
#include "Script.h"
#include "Sprite.h"

ShowBrick::ShowBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::ShowBrick, spriteReference, parent)
{
}

void ShowBrick::Execute()
{
	m_parent->Parent()->SetTransparency(1.0f);
}