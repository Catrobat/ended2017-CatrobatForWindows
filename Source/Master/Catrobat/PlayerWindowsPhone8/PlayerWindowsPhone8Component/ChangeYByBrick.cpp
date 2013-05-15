#include "pch.h"
#include "ChangeYByBrick.h"
#include "Script.h"
#include "Sprite.h"

ChangeYByBrick::ChangeYByBrick(string spriteReference, float offsetY, Script *parent) :
	Brick(TypeOfBrick::ChangeYByBrick, spriteReference, parent),
	m_offsetY(offsetY)
{
}

void ChangeYByBrick::Execute()
{
	float currentX, currentY;
	m_parent->Parent()->GetPosition(currentX, currentY);
	m_parent->Parent()->SetPosition(currentX + m_offsetY, currentY);
}