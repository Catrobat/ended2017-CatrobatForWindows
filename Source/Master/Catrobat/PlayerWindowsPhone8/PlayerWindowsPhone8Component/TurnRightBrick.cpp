#include "pch.h"
#include "TurnRightBrick.h"
#include "Script.h"
#include "Sprite.h"

TurnRightBrick::TurnRightBrick(string spriteReference, float rotation, Script *parent) :
	Brick(TypeOfBrick::TurnRightBrick, spriteReference, parent),
	m_rotation(rotation)
{
}

void TurnRightBrick::Execute()
{
	m_parent->Parent()->SetRotation(m_parent->Parent()->GetRotation() + m_rotation);
}