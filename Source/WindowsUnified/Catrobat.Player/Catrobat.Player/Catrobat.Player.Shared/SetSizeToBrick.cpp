#include "pch.h"
#include "SetSizeToBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

SetSizeToBrick::SetSizeToBrick(FormulaTree *scale, std::shared_ptr<Script> parent) :
Brick(TypeOfBrick::SetGhostEffectBrick, parent),
m_scale(scale)
{
}

void SetSizeToBrick::Execute()
{
    float scale = Interpreter::Instance()->EvaluateFormulaToFloat(m_scale, m_parent->GetParent()) / 100;
    m_parent->GetParent()->SetScale(scale, scale);
}