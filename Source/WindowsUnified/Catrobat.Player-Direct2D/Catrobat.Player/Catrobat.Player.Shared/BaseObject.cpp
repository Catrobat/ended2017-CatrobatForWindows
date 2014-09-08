#include "pch.h"
#include "BaseObject.h"

using namespace Windows::Graphics::Display;

BaseObject::BaseObject(float scaleX, float scaleY)
{
	m_objectScale.width = scaleX;
	m_objectScale.height = scaleY;
}