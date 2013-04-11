#include "pch.h"
#include "PlaceAtBrick.h"

PlaceAtBrick::PlaceAtBrick(string spriteReference, float positionX, float positionY, Script *parent) :
	Brick(TypeOfBrick::PlaceAtBrick, spriteReference, parent),
	m_positionX(positionX), m_positionY(positionY)
{
}

void PlaceAtBrick::Execute()
{
}