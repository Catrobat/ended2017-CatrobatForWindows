#include "pch.h"
#include "PointToBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

PointToBrick::PointToBrick(Catrobat_Player::NativeComponent::IPointToBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::PointToBrick, parent),
	m_rotation(make_shared<FormulaTree>(brick->Rotation))
{
}

void PointToBrick::Execute()
{
	m_parent->GetParent()->SetRotation(Interpreter::Instance()->EvaluateFormulaToFloat(m_rotation, m_parent->GetParent()));
}