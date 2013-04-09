#pragma once

#include "BaseObject.h"
#include "Script.h"

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
		Brick(TypeOfBrick brickType, string spriteReference, Script* parent);

private:
	TypeOfBrick m_brickType;
	string m_spriteReference;
	Script *m_parent;
};

