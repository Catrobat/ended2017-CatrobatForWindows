#include "pch.h"
#include "ContainerBrick.h"
#include "Interpreter.h"

ContainerBrick::ContainerBrick(TypeOfBrick brickType, string objectReference, Script *parent) :
	Brick(brickType, objectReference, parent)
{
}