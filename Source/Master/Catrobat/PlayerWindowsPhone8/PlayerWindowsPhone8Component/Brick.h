#pragma once

#include "BaseObject.h"

#include <string>

using namespace std;
class Script;

class Brick
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

	virtual void Execute() = 0;

	TypeOfBrick BrickType();

protected:
	Brick(TypeOfBrick brickType, string spriteReference, Script *parent);
	Script *m_parent;
private:
	TypeOfBrick m_brickType;
	string m_spriteReference;
};
