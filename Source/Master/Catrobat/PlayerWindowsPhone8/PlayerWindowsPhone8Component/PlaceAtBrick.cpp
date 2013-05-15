#include "pch.h"
#include "PlaceAtBrick.h"
#include "Script.h"
#include "Object.h"

PlaceAtBrick::PlaceAtBrick(string spriteReference, float positionX, float positionY, Script *parent) :
	Brick(TypeOfBrick::PlaceAtBrick, spriteReference, parent),
	m_positionX(positionX), m_positionY(positionY)
{
}

void PlaceAtBrick::Execute()
{
	m_parent->Parent()->SetPosition(m_positionX, m_positionY);
}