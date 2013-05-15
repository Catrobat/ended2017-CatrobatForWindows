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
		PlaySoundBrick,
		TurnLeftBrick,
		ForeverBrick,
		HideBrick,
		ShowBrick,
		SetSizeToBrick,
		ChangeSizeByBrick,
		TurnRightBrick,
		SetXBrick,
		SetYBrick,
		ChangeXByBrick,
		ChangeYByBrick,
		GlideToBrick,
		PointToBrick
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
