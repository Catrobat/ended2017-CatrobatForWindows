#include "pch.h"
#include "SetXBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

SetXBrick::SetXBrick(string spriteReference, FormulaTree *positionX, Script *parent) :
	Brick(TypeOfBrick::SetXBrick, spriteReference, parent),
	m_positionX(positionX)
{
}

void SetXBrick::Execute()
{
	float currentX, currentY;
	m_parent->GetParent()->GetPosition(currentX, currentY);
	m_parent->GetParent()->SetPosition(Interpreter::Instance()->EvaluateFormulaToFloat(m_positionX, m_parent->GetParent()), currentY);
}