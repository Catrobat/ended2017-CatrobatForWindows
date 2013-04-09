#include "pch.h"
#include "PlaceAtBrick.h"
#include "Script.h"

PlaceAtBrick::PlaceAtBrick(string spriteReference, Script* parent, float positionX, float positionY) :
	Brick(TypeOfBrick::PlaceAtBrick, spriteReference, parent), 
	m_positionX(positionX), m_positionY(positionY)
{
}

