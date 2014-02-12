#include "pch.h"
#include "ChangeSizeByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

ChangeSizeByBrick::ChangeSizeByBrick(FormulaTree *scale, Script *parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, parent),
	m_scale(scale)
{
}

void ChangeSizeByBrick::Execute()
{
	m_parent->GetParent()->SetScale(m_parent->GetParent()->GetScale() + Interpreter::Instance()->EvaluateFormulaToFloat(m_scale, GetParent()->GetParent()));
}