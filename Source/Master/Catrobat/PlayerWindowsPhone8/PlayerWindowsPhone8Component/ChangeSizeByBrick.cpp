#include "pch.h"
#include "ChangeSizeByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

ChangeSizeByBrick::ChangeSizeByBrick(string spriteReference, FormulaTree *scale, Script *parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, spriteReference, parent),
	m_scale(scale)
{
}

void ChangeSizeByBrick::Execute()
{
	m_parent->GetParent()->SetScale(m_parent->GetParent()->GetScale() + Interpreter::Instance()->EvaluateFormulaToFloat(m_scale, GetParent()->GetParent()));
}