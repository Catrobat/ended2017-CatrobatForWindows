#include "pch.h"
#include "SetYBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

SetYBrick::SetYBrick(string spriteReference, FormulaTree *positionY, Script *parent) :
	Brick(TypeOfBrick::SetYBrick, spriteReference, parent),
	m_positionY(positionY)
{
}

void SetYBrick::Execute()
{
	float currentX, currentY;
	m_parent->Parent()->GetPosition(currentX, currentY);
	m_parent->Parent()->SetPosition(currentX, Interpreter::Instance()->EvaluateFormulaToFloat(m_positionY, m_parent->Parent()));
}