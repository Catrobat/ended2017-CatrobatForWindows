#include "pch.h"
#include "PlaceAtBrick.h"


PlaceAtBrick::PlaceAtBrick(string spriteReference, float positionX, float positionY) :
	Brick(TypeOfBrick::PlaceAtBrick, spriteReference), 
	m_positionX(positionX), m_positionY(positionY)
{
}

