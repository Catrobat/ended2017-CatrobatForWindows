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

protected:
	BaseObject(float x, float y, float width, float height, float originX, float originY);
	BaseObject(Rect position, float originX, float originY);
	BaseObject(Point location, Size size, float originX, float originY);
	//~BaseObject(void);

	XMFLOAT2 m_position;
	XMFLOAT2 m_sourceOrigin;
	XMFLOAT2 m_objectScale;

	float scale;
};

