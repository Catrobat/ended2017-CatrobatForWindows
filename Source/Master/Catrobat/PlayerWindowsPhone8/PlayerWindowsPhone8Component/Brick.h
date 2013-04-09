#pragma once

#include "BaseObject.h"

#include <string>

using namespace std;
class Script;

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

	Script *Parent();

	void Render(SpriteBatch *spriteBatch);
	void LoadTextures(ID3D11Device* d3dDevice);
	void Draw(SpriteBatch *spriteBatch);
	void LoadTexture(ID3D11Device* d3dDevice);

	TypeOfBrick BrickType();

protected:
	Brick(TypeOfBrick brickType, string spriteReference, Script *parent);

private:
	Script *m_parent;
	TypeOfBrick m_brickType;
	string m_spriteReference;
};
