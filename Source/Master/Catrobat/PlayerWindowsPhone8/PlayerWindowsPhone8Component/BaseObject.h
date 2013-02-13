#pragma once

#include "SpriteBatch.h"

#include "DDSTextureLoader.h"
#include <D3D11.h>
#include <windows.foundation.h>

using namespace std;
using namespace DirectX;
using namespace Windows::Foundation;

class BaseObject
{
public:
	virtual void Draw(SpriteBatch *spriteBatch) = 0;
	virtual void LoadTexture(ID3D11Device* d3dDevice) = 0;

protected:
	BaseObject(float posX, float posY, Rect *windowBounds, float originX = 0, float originY = 0);
	BaseObject(float x, float y, float width, float height, float originX, float originY);
	BaseObject(Rect *position, float originX = 0, float originY = 0);
	BaseObject(Point location, Size size, float originX = 0, float originY = 0);
	~BaseObject(void);

	ID3D11ShaderResourceView* m_Texture;

	XMFLOAT2 m_position;
	XMFLOAT2 m_sourceOrigin;
	XMFLOAT2 m_objectScale;

	float scale;
};

