#include "pch.h"
#include "SetYBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

using namespace ProjectStructure;

SetYBrick::SetYBrick(FormulaTree *positionY, Script* parent) :
	Brick(TypeOfBrick::SetYBrick, parent),
	m_positionY(positionY)
{
}

void SetYBrick::Execute()
{
	float currentX, currentY;
	m_parent->GetParent()->GetTranslation(currentX, currentY);
	auto newYPosition = Interpreter::Instance()->EvaluateFormulaToFloat(m_positionY, m_parent->GetParent());
	m_parent->GetParent()->SetTranslation(currentX, newYPosition);
}