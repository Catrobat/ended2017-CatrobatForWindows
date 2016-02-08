#include "pch.h"
#include "ChangeYByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

ChangeYByBrick::ChangeYByBrick(Catrobat_Player::NativeComponent::IChangeYByBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::ChangeYByBrick, parent),
	m_offsetY(make_shared<FormulaTree>(brick->OffsetY))
{
}

void ChangeYByBrick::Execute()
{
	float currentX, currentY;
	m_parent->GetParent()->GetTranslation(currentX, currentY);
	m_parent->GetParent()->SetTranslation(currentX, currentY - Interpreter::Instance()->EvaluateFormulaToFloat(m_offsetY.get(), m_parent->GetParent()));
}