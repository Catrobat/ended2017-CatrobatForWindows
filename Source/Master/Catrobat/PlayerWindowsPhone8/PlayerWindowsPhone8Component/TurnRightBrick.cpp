#include "pch.h"
#include "TurnRightBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

TurnRightBrick::TurnRightBrick(FormulaTree *rotation, Script *parent) :
	Brick(TypeOfBrick::TurnRightBrick, parent),
	m_rotation(rotation)
{
}

void TurnRightBrick::Execute()
{
    m_parent->GetParent()->GetRotationProvider()->AddRotation(Interpreter::Instance()->EvaluateFormulaToFloat(m_rotation, m_parent->GetParent()));
	//m_parent->GetParent()->SetRotation();
}