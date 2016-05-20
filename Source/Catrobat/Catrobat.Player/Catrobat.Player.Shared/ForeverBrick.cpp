#include "pch.h"
#include "ForeverBrick.h"
#include "Interpreter.h"

using namespace std;
using namespace ProjectStructure;

ForeverBrick::ForeverBrick(Catrobat_Player::NativeComponent::IForeverBrick^ brick, Script* parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, parent)
{
}


ForeverBrick::~ForeverBrick()
{
}

void ForeverBrick::Execute()
{
	while (true)
	{
		for each (auto &brick in m_brickList)
		{
			brick->Execute();
		}
	}
}