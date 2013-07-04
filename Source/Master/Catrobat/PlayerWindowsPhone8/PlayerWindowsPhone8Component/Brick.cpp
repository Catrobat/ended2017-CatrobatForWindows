#include "pch.h"
#include "Brick.h"
#include "Script.h"
#include "Object.h"
#include "CostumeBrick.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;

Brick::Brick(TypeOfBrick brickType, string objectReference, Script *parent) :
	m_brickType(brickType), m_objectReference(objectReference), m_parent(parent)
{
}

Brick::TypeOfBrick Brick::GetBrickType()
{
	return m_brickType;
}

Script *Brick::GetParent()
{
	return m_parent;
}