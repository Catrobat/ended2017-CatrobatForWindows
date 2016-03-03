#include "pch.h"
#include "RepeatBrick.h"
#include "Interpreter.h"

using namespace std;
using namespace ProjectStructure;

RepeatBrick::RepeatBrick(Catrobat_Player::NativeComponent::IRepeatBrick^ brick, Script* parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, parent),
	m_timesToRepeat(make_shared<FormulaTree>(brick->TimesToRepeat))
{
}

RepeatBrick::~RepeatBrick()
{
}

void RepeatBrick::Execute()
{
	int global = 0;
	int times = Interpreter::Instance()->EvaluateFormulaToInt(m_timesToRepeat, GetParent()->GetParent());

	while (global < times)
	{
		for each (auto &brick in m_brickList)
		{
			brick->Execute();
		}
		global++;
		Concurrency::wait(20);
	}
}