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
	// We need to differ the two constructors which would have the same signature.
	enum TypeOfConstructor 
	{
		WidthAndHeight,
		Points
	};

	virtual void Draw(SpriteBatch *spriteBatch) = 0;
	virtual void LoadTexture(ID3D11Device* d3dDevice) = 0;

protected:
	BaseObject(void);
	BaseObject(float x, float y, Rect *windowBounds);
	BaseObject(float topX, float topY, float botX, float botY, TypeOfConstructor type);
	BaseObject(Rect *position);
	BaseObject(Point location, Size size);
	~BaseObject(void);

	ID3D11ShaderResourceView* m_Texture;

	RECT m_position;
	float scale;
};

