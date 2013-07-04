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
	m_parent->GetParent()->GetPosition(currentX, currentY);
	m_parent->GetParent()->SetPosition(currentX, currentY + Interpreter::Instance()->EvaluateFormulaToFloat(m_offsetY, m_parent->GetParent()));
}