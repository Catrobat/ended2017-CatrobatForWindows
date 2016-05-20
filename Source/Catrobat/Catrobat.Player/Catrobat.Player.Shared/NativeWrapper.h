#pragma once

#include "IProject.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public ref class NativeWrapper sealed
		{
		public:
			static void SetProject(IProject^ project);
		};

	}
}

