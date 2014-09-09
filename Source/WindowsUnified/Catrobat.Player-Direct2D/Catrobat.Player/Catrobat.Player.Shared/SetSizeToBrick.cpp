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
    float scale = Interpreter::Instance()->EvaluateFormulaToFloat(m_scale, m_parent->GetParent());
    m_parent->GetParent()->SetScale(scale, scale);
}