#include "pch.h"
#include "PlaceAtBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

PlaceAtBrick::PlaceAtBrick(FormulaTree *positionX, FormulaTree *positionY, std::shared_ptr<Script> parent) :
	Brick(TypeOfBrick::PlaceAtBrick, parent),
	m_positionX(positionX), m_positionY(positionY)
{
}

void PlaceAtBrick::Execute()
{
    auto xPosition = Interpreter::Instance()->EvaluateFormulaToFloat(m_positionX, m_parent->GetParent());
    auto yPosition = Interpreter::Instance()->EvaluateFormulaToFloat(m_positionY, m_parent->GetParent());
    m_parent->GetParent()->SetTranslation(xPosition, yPosition);
}