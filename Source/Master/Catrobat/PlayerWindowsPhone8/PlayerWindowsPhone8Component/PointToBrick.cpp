#include "pch.h"
#include "PointToBrick.h"
#include "Script.h"
#include "Object.h"
#include "Interpreter.h"

PointToBrick::PointToBrick(FormulaTree *rotation, Script *parent) :
	Brick(TypeOfBrick::PointToBrick, parent),
	m_rotation(rotation)
{
}

void PointToBrick::Execute()
{
	m_parent->GetParent()->SetRotation(Interpreter::Instance()->EvaluateFormulaToFloat(m_rotation, m_parent->GetParent()));
}