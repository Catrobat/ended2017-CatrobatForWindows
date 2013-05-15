#include "pch.h"
#include "ChangeXByBrick.h"
#include "Script.h"
#include "Sprite.h"

ChangeXByBrick::ChangeXByBrick(string spriteReference, float offsetX, Script *parent) :
	Brick(TypeOfBrick::ChangeXByBrick, spriteReference, parent),
	m_offsetX(offsetX)
{
}

void ChangeXByBrick::Execute()
{
	float currentX, currentY;
	m_parent->Parent()->GetPosition(currentX, currentY);
	m_parent->Parent()->SetPosition(currentX + m_offsetX, currentY);
}