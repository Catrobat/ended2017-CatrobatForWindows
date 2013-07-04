#include "pch.h"
#include "ShowBrick.h"
#include "Script.h"
#include "Object.h"

ShowBrick::ShowBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::ShowBrick, spriteReference, parent)
{
}

void ShowBrick::Execute()
{
	m_parent->GetParent()->SetTransparency(0.0f);
}