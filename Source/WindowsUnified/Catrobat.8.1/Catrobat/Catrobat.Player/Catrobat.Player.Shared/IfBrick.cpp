#include "pch.h"
#include "IfBrick.h"
#include "Interpreter.h"

using namespace std;
using namespace ProjectStructure;

IfBrick::IfBrick(Catrobat_Player::NativeComponent::IIfBrick^ brick, Script* parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, parent),
	m_condition(make_shared<FormulaTree>(brick->Condition)),
	m_elseMode(false)
{
}

IfBrick::~IfBrick()
{
}

std::list<std::unique_ptr<Brick>> *IfBrick::ListPointer()
{
	if (m_elseMode)
	{
		return &m_alternateBrickList;
	}
	else
	{
		return &m_brickList;
	}
}

void IfBrick::Execute()
{
	if (Interpreter::Instance()->EvaluateFormulaToBool(m_condition, GetParent()->GetParent()))
	{
		for each (auto &brick in m_brickList)
		{
			brick->Execute();
		}
	}
	else
	{
		for each (auto &brick in m_alternateBrickList)
		{
			brick->Execute();
		}
	}
}

void IfBrick::ElseMode()
{
	m_elseMode = true;
}