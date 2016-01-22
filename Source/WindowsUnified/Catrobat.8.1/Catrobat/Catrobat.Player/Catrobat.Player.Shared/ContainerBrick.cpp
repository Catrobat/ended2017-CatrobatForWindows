#include "pch.h"
#include "ContainerBrick.h"
#include "Interpreter.h"

using namespace ProjectStructure;

ContainerBrick::ContainerBrick(TypeOfBrick brickType, Catrobat_Player::NativeComponent::IContainerBrick^ brick, Script* parent) :
	Brick(brickType, parent)
{
	// TODO: Bricklist
}