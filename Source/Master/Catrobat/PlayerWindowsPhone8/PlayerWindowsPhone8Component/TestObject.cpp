#include "pch.h"
#include "TestObject.h"

TestObject::TestObject()
{
}

void TestObject::Draw(SpriteBatch *spriteBatch)
{
	spriteBatch->Draw(m_Texture, m_position, Colors::White);
	XMFLOAT2 test1;
	XMFLOAT2 test2;
	XMFLOAT2 test3;
	test1.x = 1.1f;
	test1.y = 50.5f;
	test2.x = 80.8f;
	test2.y = 90.9f;
	test3.x = 80.8f;
	test3.y = 90.9f;
	
	spriteBatch->Draw(m_Texture, test1, nullptr, Colors::Wheat, 0.0f, test2, test3, SpriteEffects_None, 0.0f);
}

void TestObject::LoadTexture(ID3D11Device* d3dDevice)
{
	CreateDDSTextureFromFile(d3dDevice, L"CatTexture.dds", nullptr, &m_Texture, MAXSIZE_T);
}