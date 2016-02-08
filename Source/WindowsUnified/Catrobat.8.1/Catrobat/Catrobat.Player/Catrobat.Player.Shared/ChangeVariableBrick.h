#pragma once

#include "VariableManagementBrick.h"
#include "IChangeVariableBrick.h"

namespace ProjectStructure
{
	class ChangeVariableBrick
		: public VariableManagementBrick
	{
	public:
		ChangeVariableBrick(Catrobat_Player::NativeComponent::IChangeVariableBrick^ brick, Script* parent);
		void Execute();
	};
}