#include "pch.h"
#include "SetGhostEffectBrick.h"


SetGhostEffectBrick::SetGhostEffectBrick(string spriteReference, float transparency) :
	Brick(TypeOfBrick::SetGhostEffectBrick, spriteReference),
	m_transpareny(transparency)
{
}

