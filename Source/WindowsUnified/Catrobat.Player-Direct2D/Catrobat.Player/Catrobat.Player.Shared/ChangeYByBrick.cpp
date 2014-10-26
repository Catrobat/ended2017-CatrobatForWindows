#include "pch.h"
#include "ChangeYByBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

ChangeYByBrick::ChangeYByBrick(FormulaTree *offsetY, std::shared_ptr<Script> parent) :
	Brick(TypeOfBrick::ChangeYByBrick, parent),
	m_offsetY(offsetY)
{
}

void ChangeYByBrick::Execute()
{
	float currentX, currentY;
    m_parent->GetParent()->GetTranslation(currentX, currentY);
    m_parent->GetParent()->SetTranslation(currentX, currentY - Interpreter::Instance()->EvaluateFormulaToFloat(m_offsetY, m_parent->GetParent()));
}