#include "pch.h"
#include "ChangeXByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

ChangeXByBrick::ChangeXByBrick(string spriteReference, FormulaTree *offsetX, Script *parent) :
	Brick(TypeOfBrick::ChangeXByBrick, spriteReference, parent),
	m_offsetX(offsetX)
{
}

void ChangeXByBrick::Execute()
{
	float currentX, currentY;
	m_parent->Parent()->GetPosition(currentX, currentY);
	m_parent->Parent()->SetPosition(currentX + Interpreter::Instance()->EvaluateFormulaToFloat(m_offsetX, m_parent->Parent()), currentY);
}