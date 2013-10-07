#include "pch.h"
#include "SetSizeToBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

SetSizeToBrick::SetSizeToBrick(FormulaTree *scale, Script *parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, parent),
	m_scale(scale)
{
}

void SetSizeToBrick::Execute()
{
	m_parent->GetParent()->SetScale(Interpreter::Instance()->EvaluateFormulaToFloat(m_scale, m_parent->GetParent()));
}