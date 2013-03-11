#include "pch.h"
#include "Brick.h"


Brick::Brick(TypeOfBrick brickType, string spriteReference) :
	m_brickType(brickType), m_spriteReference(spriteReference)
{
}

Brick::TypeOfBrick Brick::BrickType()
{
	return m_brickType;
}
