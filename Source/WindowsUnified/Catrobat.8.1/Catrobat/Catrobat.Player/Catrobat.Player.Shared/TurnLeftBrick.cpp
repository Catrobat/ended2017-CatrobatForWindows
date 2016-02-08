#include "pch.h"
#include "TurnLeftBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

TurnLeftBrick::TurnLeftBrick(Catrobat_Player::NativeComponent::ITurnLeftBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::TurnLeftBrick, parent),
	m_rotation(make_shared<FormulaTree>(brick->Rotation))
{
}

void TurnLeftBrick::Execute()
{
	auto rotation = m_parent->GetParent()->GetRotation();
	rotation -= Interpreter::Instance()->EvaluateFormulaToFloat(m_rotation.get(), m_parent->GetParent());
	rotation = rotation >= 0 ? rotation : 360 + rotation; //rotation has to be between 0 and 360
	m_parent->GetParent()->SetRotation(rotation);
}