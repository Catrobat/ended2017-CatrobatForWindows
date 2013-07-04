#include "pch.h"
#include "TurnRightBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

TurnRightBrick::TurnRightBrick(string spriteReference, FormulaTree *rotation, Script *parent) :
	Brick(TypeOfBrick::TurnRightBrick, spriteReference, parent),
	m_rotation(rotation)
{
}

void TurnRightBrick::Execute()
{
	m_parent->GetParent()->SetRotation(m_parent->GetParent()->GetRotation() + Interpreter::Instance()->EvaluateFormulaToFloat(m_rotation, m_parent->GetParent()));
}