#include "pch.h"
#include "TurnRightBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;

TurnRightBrick::TurnRightBrick(FormulaTree *rotation,std::shared_ptr<Script> parent) :
	Brick(TypeOfBrick::TurnRightBrick, parent),
	m_rotation(rotation)
{
}

void TurnRightBrick::Execute()
{
	auto rotation = m_parent->GetParent()->GetRotation();
	rotation += Interpreter::Instance()->EvaluateFormulaToFloat(m_rotation, m_parent->GetParent());
	rotation = (rotation > 360) ? rotation - 360 : rotation; //rotation not greater than 360
	m_parent->GetParent()->SetRotation(rotation);
}