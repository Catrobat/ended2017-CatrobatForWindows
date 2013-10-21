#include "pch.h"
#include "TurnLeftBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

TurnLeftBrick::TurnLeftBrick(FormulaTree *rotation, Script *parent) :
	Brick(TypeOfBrick::TurnLeftBrick, parent),
	m_rotation(rotation)
{
}

void TurnLeftBrick::Execute()
{
    //m_parent->GetParent()->SetRotation(Interpreter::Instance()->GetRotationProvider(m_rotation, m_parent->GetParent()));

    m_parent->GetParent()->GetRotationProvider()->AddRotation((-1) * Interpreter::Instance()->EvaluateFormulaToFloat(m_rotation, m_parent->GetParent()));
	//m_parent->GetParent()->SetRotation(m_parent->GetParent()->GetRotation() - Interpreter::Instance()->GetRotationProvider(m_rotation, m_parent->GetParent())->GetRotation());
}