#include "pch.h"
#include "TestObject.h"

TestObject::TestObject(float x, float y, Rect *windowBounds) : 
	BaseObject(x, y, windowBounds)
{
}

void TestObject::Draw(SpriteBatch *spriteBatch)
{
	spriteBatch->Draw(m_Texture, XMFLOAT2(posX, posY), nullptr, Colors::White, 0.0f, XMFLOAT2(250.0f, 250.0f), XMFLOAT2(diameter / 500.0f, diameter / 500.0f), DirectX::SpriteEffects_None, 0.0f);
}

void TestObject::LoadTexture(ID3D11Device* d3dDevice)
{
	CreateDDSTextureFromFile(d3dDevice, L"CatTexture.dds", nullptr, &m_Texture, MAXSIZE_T);
}