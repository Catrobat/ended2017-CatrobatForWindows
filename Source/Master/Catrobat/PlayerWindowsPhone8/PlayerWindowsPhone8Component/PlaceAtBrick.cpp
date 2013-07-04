#include "pch.h"
#include "PlaceAtBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

PlaceAtBrick::PlaceAtBrick(string spriteReference, FormulaTree *positionX, FormulaTree *positionY, Script *parent) :
	Brick(TypeOfBrick::PlaceAtBrick, spriteReference, parent),
	m_positionX(positionX), m_positionY(positionY)
{
}

void PlaceAtBrick::Execute()
{
	m_parent->GetParent()->SetPosition(Interpreter::Instance()->EvaluateFormulaToFloat(m_positionX, m_parent->GetParent()), Interpreter::Instance()->EvaluateFormulaToFloat(m_positionY, m_parent->GetParent()));
}