#include "pch.h"
#include "RepeatBrick.h"
#include "Interpreter.h"

using namespace std;
using namespace ProjectStructure;

RepeatBrick::RepeatBrick(FormulaTree *times, Script* parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, parent), m_timesToRepeat(times)
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

void RepeatBrick::AddBrick(unique_ptr<Brick> brick)
{
	m_brickList.push_back(move(brick));
}