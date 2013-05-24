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
	m_parent->Parent()->SetScale(m_parent->Parent()->GetScale() + Interpreter::Instance()->EvaluateFormulaToFloat(m_scale));
}