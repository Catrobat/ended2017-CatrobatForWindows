#include "pch.h"
#include "ChangeSizeByBrick.h"
#include "Script.h"
#include "Sprite.h"

ChangeSizeByBrick::ChangeSizeByBrick(string spriteReference, float scale, Script *parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, spriteReference, parent),
	m_scale(scale)
{
}

void ChangeSizeByBrick::Execute()
{
	m_parent->Parent()->SetScale(m_parent->Parent()->GetScale() + m_scale);
}