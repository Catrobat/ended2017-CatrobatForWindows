#include "pch.h"
#include "BaseObject.h"

using namespace Windows::Graphics::Display;

BaseObject::BaseObject(void)
{
}

BaseObject::BaseObject(float x, float y, Rect *windowBounds)
{
	posX = x;
	posY = y;
	scale = DisplayProperties::LogicalDpi / 96.0f;
	diameter = windowBounds->Width / 10.0f * scale;

	m_Texture = nullptr;
}

void BaseObject::Draw(SpriteBatch *spriteBatch)
{
	spriteBatch->Draw(m_Texture, XMFLOAT2(posX, posY), nullptr, Colors::White, 0.0f, XMFLOAT2(250.0f, 250.0f), XMFLOAT2(diameter / 500.0f, diameter / 500.0f), DirectX::SpriteEffects_None, 0.0f);
}

void BaseObject::LoadTexture(ID3D11Device* d3dDevice)
{
	//CreateDDSTextureFromFile(d3dDevice, L"FILENAME.dds", nullptr, &m_Texture, MAXSIZE_T);
}

