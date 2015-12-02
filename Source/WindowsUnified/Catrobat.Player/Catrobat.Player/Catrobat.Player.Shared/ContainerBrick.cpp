#include "pch.h"
#include "ContainerBrick.h"
#include "Interpreter.h"

using namespace ProjectStructure;

ContainerBrick::ContainerBrick(TypeOfBrick brickType, std::shared_ptr<Script>parent) :
	Brick(brickType, parent)
{
}