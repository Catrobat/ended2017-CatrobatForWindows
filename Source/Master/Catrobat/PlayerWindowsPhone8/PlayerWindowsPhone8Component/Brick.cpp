#include "pch.h"
#include "Brick.h"

Brick::Brick(TypeOfBrick brickType, string spriteReference, Script *parent) :
	m_brickType(brickType), m_spriteReference(spriteReference), m_parent(parent)
{
}

Brick::TypeOfBrick Brick::BrickType()
{
	return m_brickType;
}

void Brick::Render(SpriteBatch *spriteBatch)
{

}

Script *Brick::Parent()
{
	return m_parent;
}