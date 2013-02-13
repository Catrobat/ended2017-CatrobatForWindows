#pragma once

#include "baseobject.h"

class TestObject :
	public BaseObject
{
public:
	TestObject(void);
	TestObject(float x, float y, Rect *windowBounds);

	void Draw(SpriteBatch *spriteBatch);
	void LoadTexture(ID3D11Device* d3dDevice);

private:
	~TestObject(void);
};

