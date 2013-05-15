#include "pch.h"
#include "TurnLeftBrick.h"
#include "Script.h"
#include "Sprite.h"

TurnLeftBrick::TurnLeftBrick(string spriteReference, float rotation, Script *parent) :
	Brick(TypeOfBrick::TurnLeftBrick, spriteReference, parent),
	m_rotation(rotation)
{
}

void TurnLeftBrick::Execute()
{
	m_parent->Parent()->SetRotation(m_rotation);
}