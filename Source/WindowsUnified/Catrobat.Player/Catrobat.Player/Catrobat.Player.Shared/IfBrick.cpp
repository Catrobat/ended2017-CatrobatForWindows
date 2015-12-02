#include "pch.h"
#include "IfBrick.h"
#include "Interpreter.h"

using namespace std;
using namespace ProjectStructure;

IfBrick::IfBrick(FormulaTree *condition, Script* parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, parent), m_condition(condition)
{
	m_currentAddMode = IfBranchType::If;
}


IfBrick::~IfBrick()
{
}

void IfBrick::Execute()
{
	// Synchronously execute all subsequent blocks
	if (Interpreter::Instance()->EvaluateFormulaToBool(m_condition, GetParent()->GetParent()))
	{
		for each (auto &brick in m_ifList)
		{
			brick->Execute();
		}
	}
	else
	{
		for each (auto &brick in m_elseList)
		{
			brick->Execute();
		}
	}
}

void IfBrick::AddBrick(unique_ptr<Brick> brick)
{
	if (m_currentAddMode == IfBranchType::If)
	{
		m_ifList.push_back(move(brick));
	}
	else
	{
		m_elseList.push_back(move(brick));
	}
}

void IfBrick::SetCurrentAddMode(IfBranchType mode)
{
	m_currentAddMode = mode;
}