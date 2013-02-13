#pragma once

#include "baseobject.h"

class TestObject :
	public BaseObject
{
public:
	TestObject(float posX, float posY, Rect *windowBounds, float originX, float originY);
	TestObject(float x, float y, float width, float height, float originX, float originY);
	TestObject(Rect *position, float originX = 0, float originY = 0);
	TestObject(Point location, Size size, float originX = 0, float originY = 0);

	void Draw(SpriteBatch *spriteBatch);
	void LoadTexture(ID3D11Device* d3dDevice);

private:
	~TestObject(void);
};

