#include "pch.h"
#include "SetXBrick.h"
#include "Script.h"
#include "Object.h"

SetXBrick::SetXBrick(string spriteReference, float positionX, Script *parent) :
	Brick(TypeOfBrick::SetXBrick, spriteReference, parent),
	m_positionX(positionX)
{
}

void SetXBrick::Execute()
{
	float currentX, currentY;
	m_parent->Parent()->GetPosition(currentX, currentY);
	m_parent->Parent()->SetPosition(m_positionX, currentY);
}