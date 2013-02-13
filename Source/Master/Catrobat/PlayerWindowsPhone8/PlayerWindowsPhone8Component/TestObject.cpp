#include "pch.h"
#include "TestObject.h"

TestObject::TestObject(float posX, float posY, Rect *windowBounds, float originX, float originY) :
	BaseObject(posX, posY, windowBounds, originX, originY)
{
}

TestObject::TestObject(float x, float y, float width, float height, float originX, float originY) :
	BaseObject(x, y, width, height, originX, originY)
{
}

TestObject::TestObject(Rect *position, float originX , float originY) :
	BaseObject(position, originX, originY)
{
}

TestObject::TestObject(Point location, Size size, float originX, float originY) :
	BaseObject(location, size, originX, originY)
{
}

void TestObject::Draw(SpriteBatch *spriteBatch)
{
	spriteBatch->Draw(m_Texture, m_position, nullptr, Colors::Wheat, 0.0f, m_sourceOrigin, m_objectScale, SpriteEffects_None, 0.0f);
}

void TestObject::LoadTexture(ID3D11Device* d3dDevice)
{
	CreateDDSTextureFromFile(d3dDevice, L"CatTexture.dds", nullptr, &m_Texture, MAXSIZE_T);
}