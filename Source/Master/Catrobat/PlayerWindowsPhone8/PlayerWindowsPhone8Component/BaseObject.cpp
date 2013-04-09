#include "pch.h"
#include "BaseObject.h"

using namespace Windows::Graphics::Display;

BaseObject::BaseObject(Rect position, float originX, float originY)
{
	scale = DisplayProperties::LogicalDpi / 96.0f;

	m_position.x = position.X;
	m_position.y =  position.Y;
	m_sourceOrigin.x = originX;
	m_sourceOrigin.y = originY;
	m_objectScale.x = position.Width;
	m_objectScale.y = position.Height;
}

BaseObject::BaseObject(Point location, Size size, float originX, float originY)
{
	scale = DisplayProperties::LogicalDpi / 96.0f;

	m_position.x = location.X;
	m_position.y =  location.Y;
	m_sourceOrigin.x = originX;
	m_sourceOrigin.y = originY;
	m_objectScale.x = size.Width;
	m_objectScale.y = size.Height;
}

BaseObject::BaseObject(float x, float y, float width, float height, float originX, float originY)
{
	m_position.x = x;
	m_position.y =  y;
	m_sourceOrigin.x = originX;
	m_sourceOrigin.y = originY;
	m_objectScale.x = width;
	m_objectScale.y = height;
}