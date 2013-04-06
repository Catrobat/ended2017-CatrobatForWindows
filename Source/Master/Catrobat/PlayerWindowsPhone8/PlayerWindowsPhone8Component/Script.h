#pragma once

#include "Brick.h"
#include "BaseObject.h"

#include <list>

using namespace std;

class Script : BaseObject
{
public:
	enum TypeOfScript
	{
		StartScript,
		BroadcastScript,
		WhenScript
	};

	void addBrick(Brick *brick);
	void addSpriteReference(string spriteReference);

	void Render(SpriteBatch *spriteBatch);
	void LoadTextures(ID3D11Device* d3dDevice);
	void Draw(SpriteBatch *spriteBatch);
	void LoadTexture(ID3D11Device* d3dDevice);

	string SpriteReference();

	int BrickListSize();
	Brick *GetBrick(int index);

	TypeOfScript getType();

protected:
	Script(TypeOfScript scriptType, string spriteReference);
	Script(Point location, Size size, float originX = 0, float originY = 0);

private:
	TypeOfScript m_scriptType;
	list<Brick*> *m_brickList;
	string m_spriteReference;
};

