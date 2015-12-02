#include "pch.h"
#include "ShowBrick.h"
#include "Script.h"
#include "Object.h"

using namespace ProjectStructure;

ShowBrick::ShowBrick(std::shared_ptr<Script> parent) :
	Brick(TypeOfBrick::ShowBrick, parent)
{
}

void ShowBrick::Execute()
{
	m_parent->GetParent()->SetTransparency(0.0f);
}