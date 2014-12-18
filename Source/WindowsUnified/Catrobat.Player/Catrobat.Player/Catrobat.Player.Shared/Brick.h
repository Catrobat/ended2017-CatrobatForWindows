#pragma once

#include "Object.h"
#include "FormulaTree.h"

#include <string>

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
		MoveNStepsBrick,
		PointToBrick,
		BroadcastBrick,
		IfBrick,
		ContainerBrick,
		ChangeGhostEffectByBrick,
		NextlookBrick,
		SetVariableBrick
	};

	std::shared_ptr<Script> GetParent();

	virtual void Execute() = 0;

	TypeOfBrick GetBrickType();

protected:
	Brick(TypeOfBrick brickType, std::shared_ptr<Script> parent);
	std::shared_ptr<Script> m_parent;

private:
	TypeOfBrick m_brickType;
};
