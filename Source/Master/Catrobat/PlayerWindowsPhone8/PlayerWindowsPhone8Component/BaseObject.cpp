#include "pch.h"
#include "BaseObject.h"

using namespace Windows::Graphics::Display;

BaseObject::BaseObject()
{
	scale = DisplayProperties::LogicalDpi / 96.0f;
	m_Texture = nullptr;
}

BaseObject::BaseObject(float x, float y, Rect *windowBounds)
{
	scale = DisplayProperties::LogicalDpi / 96.0f;
	diameter = windowBounds->Width / 10.0f * scale;

BaseObject::BaseObject(Rect *position)
{
	scale = DisplayProperties::LogicalDpi / 96.0f;
	m_Texture = nullptr;

	m_position.left = position->Left;
	m_position.top = position->Right;
	m_position.right = position->Right;
	m_position.bottom = position->Bottom;
}

BaseObject::BaseObject(Point location, Size size)
{
	scale = DisplayProperties::LogicalDpi / 96.0f;
	m_Texture = nullptr;
	
	m_position.left = location.X;
	m_position.top = location.Y;
	m_position.right = location.X + size.Width;
	m_position.bottom = location.Y + size.Height;
}