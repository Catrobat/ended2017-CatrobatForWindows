#include "pch.h"
#include "PointToBrick.h"
#include "Script.h"
#include "Sprite.h"

PointToBrick::PointToBrick(string spriteReference, float rotation, Script *parent) :
	Brick(TypeOfBrick::PointToBrick, spriteReference, parent),
	m_rotation(rotation)
{
}

void PointToBrick::Execute()
{
	m_parent->Parent()->SetRotation(m_rotation);
}