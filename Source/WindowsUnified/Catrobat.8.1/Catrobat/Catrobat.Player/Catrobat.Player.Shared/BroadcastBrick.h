#pragma once

#include "Brick.h"
#include "IBroadcastBrick.h"

namespace ProjectStructure
{
	class BroadcastBrick :
		public Brick
	{
	public:
		BroadcastBrick(Catrobat_Player::NativeComponent::IBroadcastBrick^ brick, Script* parent);
		void Execute();

	private:
		std::string m_broadcastMessage;
	};
}