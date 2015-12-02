#include "pch.h"
#include "ForeverBrick.h"
#include "Interpreter.h"

using namespace std;
using namespace ProjectStructure;

ForeverBrick::ForeverBrick(std::shared_ptr<Script> parent) :
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

void ForeverBrick::AddBrick(unique_ptr<Brick> brick)
{
	m_brickList.push_back(move(brick));
}