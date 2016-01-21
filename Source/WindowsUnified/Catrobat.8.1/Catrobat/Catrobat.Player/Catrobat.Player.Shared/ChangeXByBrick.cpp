#include "pch.h"
#include "ChangeXByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

ChangeXByBrick::ChangeXByBrick(Catrobat_Player::NativeComponent::IChangeXByBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::ChangeXByBrick, parent),
	m_offsetX(make_shared<FormulaTree>(brick->OffsetX))
{
}

void ChangeXByBrick::Execute()
{
	float currentX, currentY;
	m_parent->GetParent()->GetTranslation(currentX, currentY);
	m_parent->GetParent()->SetTranslation(currentX + Interpreter::Instance()->EvaluateFormulaToFloat(m_offsetX.get(), m_parent->GetParent()), currentY);
}