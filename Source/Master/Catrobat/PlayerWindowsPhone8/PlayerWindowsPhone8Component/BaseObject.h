#pragma once

#include "SpriteBatch.h"

#include "DDSTextureLoader.h"
#include <D3D11.h>

using namespace std;
using namespace DirectX;
using namespace Windows::Foundation;

class BaseObject
{
public:
	virtual void Draw(SpriteBatch *spriteBatch) = 0;
	virtual void LoadTexture(ID3D11Device* d3dDevice) = 0;

	float posX;
	float posY;
	float diameter;

protected:
	BaseObject(void);
	BaseObject(float x, float y, Rect *windowBounds);
	~BaseObject(void);

	ID3D11ShaderResourceView* m_Texture;

	// Scale with pixels
	float scale;
};

