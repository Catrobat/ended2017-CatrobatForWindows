#pragma once

#include "Object.h"
#include "FormulaTree.h"

#include <string>

namespace ProjectStructure
{
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

		Script* GetParent();

		virtual void Execute() = 0;

		TypeOfBrick GetBrickType();

	protected:
		Brick(TypeOfBrick brickType, Script* parent);
		Script* m_parent;

	private:
		TypeOfBrick m_brickType;
	};
}