#include "pch.h"
#include "ForeverBrick.h"


ForeverBrick::ForeverBrick(string spriteReference, Script *parent) :
	Brick(TypeOfBrick::ForeverBrick, spriteReference, parent)
{
}

void ForeverBrick::Execute()
{
}