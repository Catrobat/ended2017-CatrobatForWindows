#pragma once

#include "VariableManagementBrick.h"
#include "ISetVariableBrick.h"

namespace ProjectStructure
{
	class SetVariableBrick
		: public VariableManagementBrick
	{
	public:
		SetVariableBrick(Catrobat_Player::NativeComponent::ISetVariableBrick^ brick, Script* parent);
		void Execute();
	};
}