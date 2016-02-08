#include "pch.h"
#include "IfBrick.h"
#include "Interpreter.h"

using namespace std;
using namespace ProjectStructure;

IfBrick::IfBrick(Catrobat_Player::NativeComponent::IIfBrick^ brick, Script* parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, brick, parent),
	m_condition(make_shared<FormulaTree>(brick->Condition))
{
}

IfBrick::~IfBrick()
{
}

void IfBrick::Execute()
{
	// Synchronously execute all subsequent blocks

	// TODO: Use the same list.

	//if (Interpreter::Instance()->EvaluateFormulaToBool(m_condition.get(), GetParent()->GetParent()))
	//{
	//	for each (auto &brick in m_brickList)
	//	{
	//		brick->Execute();
	//	}
	//}
	//else
	//{
	//	for each (auto &brick in m_elseList)
	//	{
	//		brick->Execute();
	//	}
	//}
}