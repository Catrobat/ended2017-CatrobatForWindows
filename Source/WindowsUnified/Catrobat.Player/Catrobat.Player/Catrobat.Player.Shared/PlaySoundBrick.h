#pragma once

#include "Brick.h"

namespace ProjectStructure
{
	class PlaySoundBrick :
		public Brick
	{
	public:
		PlaySoundBrick(std::string filename, std::string name, std::shared_ptr<Script> parent);
		void Execute();
	private:
		std::string m_filename;
		std::string m_name;
	};
}