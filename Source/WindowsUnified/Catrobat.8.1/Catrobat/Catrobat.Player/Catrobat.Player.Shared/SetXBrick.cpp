#include "pch.h"
#include "SetXBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

SetXBrick::SetXBrick(Catrobat_Player::NativeComponent::ISetXBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::SetXBrick, parent),
	m_positionX(make_shared<FormulaTree>(brick->PositionX))
{
}

void SetXBrick::Execute()
{
	float currentX, currentY;
	m_parent->GetParent()->GetTranslation(currentX, currentY);
	m_parent->GetParent()->SetTranslation(Interpreter::Instance()->EvaluateFormulaToFloat(m_positionX.get(), m_parent->GetParent()), currentY);
}