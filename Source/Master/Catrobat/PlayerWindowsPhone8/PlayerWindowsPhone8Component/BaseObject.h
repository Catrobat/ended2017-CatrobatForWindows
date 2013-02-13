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
	BaseObject(void);
	BaseObject(float x, float y, Rect *windowBounds);

	// Make these and the whole class pure virtual later.
	void Draw(SpriteBatch *spriteBatch);
	void LoadTexture(ID3D11Device* d3dDevice);

	float posX;
	float posY;
	float diameter;

private:
	~BaseObject(void);

	ID3D11ShaderResourceView* m_Texture;

	// Scale with pixels
	float scale;
};

