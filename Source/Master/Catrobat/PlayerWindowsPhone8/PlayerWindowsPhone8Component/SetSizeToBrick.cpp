#include "pch.h"
#include "SetSizeToBrick.h"
#include "Script.h"
#include "Object.h"

SetSizeToBrick::SetSizeToBrick(string spriteReference, float scale, Script *parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, spriteReference, parent),
	m_scale(scale)
{
}

void SetSizeToBrick::Execute()
{
	m_parent->Parent()->SetScale(m_scale);
}