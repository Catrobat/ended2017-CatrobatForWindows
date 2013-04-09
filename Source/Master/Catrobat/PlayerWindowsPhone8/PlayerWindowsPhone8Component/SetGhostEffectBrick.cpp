#include "pch.h"
#include "SetGhostEffectBrick.h"
#include "Script.h"

SetGhostEffectBrick::SetGhostEffectBrick(string spriteReference, Script* parent, float transparency) :
	Brick(TypeOfBrick::SetGhostEffectBrick, spriteReference, parent),
	m_transpareny(transparency)
{
}

