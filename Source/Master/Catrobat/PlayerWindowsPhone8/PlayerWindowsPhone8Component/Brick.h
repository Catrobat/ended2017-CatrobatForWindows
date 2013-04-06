#pragma once

#include "BaseObject.h"

#include <string>

using namespace std;

class Brick : BaseObject
{
public:
	enum TypeOfBrick
	{
		CostumeBrick,
		WaitBrick,
		PlaceAtBrick,
		SetGhostEffectBrick,
		PlaySoundBrick
	};

	void Render(SpriteBatch *spriteBatch);
	void LoadTextures(ID3D11Device* d3dDevice);
	void Draw(SpriteBatch *spriteBatch);
	void LoadTexture(ID3D11Device* d3dDevice);
	
	TypeOfBrick BrickType();

protected:
		Brick(TypeOfBrick brickType, string spriteReference);

private:
	TypeOfBrick m_brickType;
	string m_spriteReference;
};

