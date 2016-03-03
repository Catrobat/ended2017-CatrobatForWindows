#include "pch.h"
#include "PlaceAtBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

PlaceAtBrick::PlaceAtBrick(Catrobat_Player::NativeComponent::IPlaceAtBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::PlaceAtBrick, parent),
	m_positionX(make_shared<FormulaTree>(brick->PositionX)),
	m_positionY(make_shared<FormulaTree>(brick->PositionY))
{
}

void PlaceAtBrick::Execute()
{
	auto xPosition = Interpreter::Instance()->EvaluateFormulaToFloat(m_positionX, m_parent->GetParent());
	auto yPosition = Interpreter::Instance()->EvaluateFormulaToFloat(m_positionY, m_parent->GetParent());
	m_parent->GetParent()->SetTranslation(xPosition, yPosition);
}