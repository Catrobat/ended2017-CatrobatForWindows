#include "pch.h"
#include "ChangeGhostEffectByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;

ChangeGhostEffectByBrick::ChangeGhostEffectByBrick(FormulaTree *transparency, Script* parent) :
	Brick(TypeOfBrick::ChangeGhostEffectByBrick, parent),
	m_transparency(transparency)
{
}

void ChangeGhostEffectByBrick::Execute()
{
	m_parent->GetParent()->SetTransparency(m_parent->GetParent()->GetTransparency() + (Interpreter::Instance()->EvaluateFormulaToFloat(m_transparency, GetParent()->GetParent()) / 100.f));
}