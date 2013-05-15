#include "pch.h"
#include "SetGhostEffectBrick.h"
#include "Script.h"
#include "Object.h"

SetGhostEffectBrick::SetGhostEffectBrick(string spriteReference, float transparency, Script *parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, spriteReference, parent),
	m_transparency(transparency)
{
}

void SetGhostEffectBrick::Execute()
{
	m_parent->Parent()->SetTransparency(m_transparency / 100.0f);
}