#include "pch.h"
#include "PointToBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

PointToBrick::PointToBrick(string spriteReference, FormulaTree *rotation, Script *parent) :
	Brick(TypeOfBrick::PointToBrick, spriteReference, parent),
	m_rotation(rotation)
{
}

void PointToBrick::Execute()
{
	m_parent->Parent()->SetRotation(Interpreter::Instance()->EvaluateFormulaToFloat(m_rotation, m_parent->Parent()));
}