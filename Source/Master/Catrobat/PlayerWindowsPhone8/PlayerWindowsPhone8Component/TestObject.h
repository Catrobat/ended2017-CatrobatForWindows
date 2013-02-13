#pragma once

#include "baseobject.h"

class TestObject :
	public BaseObject
{
public:
	TestObject(void);

	void Draw(SpriteBatch *spriteBatch);
	void LoadTexture(ID3D11Device* d3dDevice);

private:
	~TestObject(void);
};

