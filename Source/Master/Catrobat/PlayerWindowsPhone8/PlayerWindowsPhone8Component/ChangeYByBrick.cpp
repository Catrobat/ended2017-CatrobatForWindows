#include "pch.h"
#include "ChangeYByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

ChangeYByBrick::ChangeYByBrick(string spriteReference, FormulaTree *offsetY, Script *parent) :
	Brick(TypeOfBrick::ChangeYByBrick, spriteReference, parent),
	m_offsetY(offsetY)
{
}

void ChangeYByBrick::Execute()
{
	float currentX, currentY;
	m_parent->Parent()->GetPosition(currentX, currentY);
	m_parent->Parent()->SetPosition(currentX + Interpreter::Instance()->EvaluateFormulaToFloat(m_offsetY, m_parent->Parent()), currentY);
}