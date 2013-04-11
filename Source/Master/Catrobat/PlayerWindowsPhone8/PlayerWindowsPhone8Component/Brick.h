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

	void Execute();

	TypeOfBrick BrickType();

protected:
	Brick(TypeOfBrick brickType, string spriteReference, Script *parent);

private:
	Script *m_parent;
	TypeOfBrick m_brickType;
	string m_spriteReference;
};
