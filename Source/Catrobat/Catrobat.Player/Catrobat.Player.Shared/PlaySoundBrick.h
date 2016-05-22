#pragma once

#include "Brick.h"
#include "IPlaySoundBrick.h"

namespace ProjectStructure
{
	class PlaySoundBrick :
		public Brick
	{
	public:
		PlaySoundBrick(Catrobat_Player::NativeComponent::IPlaySoundBrick^ brick, Script* parent);
		void Execute();
	private:
		std::string m_filename;
		std::string m_name;
	};
}