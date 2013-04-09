#include "pch.h"
#include "SetGhostEffectBrick.h"

SetGhostEffectBrick::SetGhostEffectBrick(string spriteReference, float transparency, Script *parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, spriteReference, parent),
	m_transpareny(transparency)
{
}