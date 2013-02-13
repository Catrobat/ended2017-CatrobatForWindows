#include "pch.h"
#include "BaseObject.h"

using namespace Windows::Graphics::Display;

BaseObject::BaseObject(float x, float y, Rect *windowBounds)
{
	posX = x;
	posY = y;
	scale = DisplayProperties::LogicalDpi / 96.0f;
	diameter = windowBounds->Width / 10.0f * scale;

	m_Texture = nullptr;
}

