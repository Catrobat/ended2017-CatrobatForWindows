#include "pch.h"
#include "ChangeSizeByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;

ChangeSizeByBrick::ChangeSizeByBrick(FormulaTree *scale, Script* parent) :
	Brick(TypeOfBrick::SetGhostEffectBrick, parent),
	m_scale(scale)
{
}

void ChangeSizeByBrick::Execute()
{
	float scale;
	m_parent->GetParent()->GetScale(scale, scale);
	scale += Interpreter::Instance()->EvaluateFormulaToFloat(m_scale, GetParent()->GetParent());
	m_parent->GetParent()->SetScale(scale, scale);
}