#include "pch.h"
#include "Brick.h"
#include "Script.h"
#include "Object.h"
#include "CostumeBrick.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;

Brick::Brick(TypeOfBrick brickType, std::shared_ptr<Script> parent) :
	m_brickType(brickType), m_parent(parent)
{
}

Brick::TypeOfBrick Brick::GetBrickType()
{
	return m_brickType;
}

std::shared_ptr<Script> Brick::GetParent()
{
	return m_parent;
}