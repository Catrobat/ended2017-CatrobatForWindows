#include "pch.h"
#include "WaitBrick.h"
#include <windows.h>
#include <ppltasks.h>
#include "Interpreter.h"

using namespace ProjectStructure;
using namespace std;

WaitBrick::WaitBrick(Catrobat_Player::NativeComponent::IWaitBrick^ brick, Script* parent) :
	Brick(TypeOfBrick::WaitBrick, parent), m_timeToWaitInSeconds(make_shared<FormulaTree>(brick->TimeToWaitInSeconds))
{
}

void WaitBrick::Execute()
{
	Concurrency::wait(1000 * Interpreter::Instance()->EvaluateFormulaToInt(m_timeToWaitInSeconds.get(), GetParent()->GetParent()));
}