#include "pch.h"
#include "BaseObject.h"

using namespace Windows::Graphics::Display;

BaseObject::BaseObject(float scaleX, float scaleY)
{
	m_objectScale.x = scaleX;
	m_objectScale.y = scaleY;
}