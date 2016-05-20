#pragma once

#include "IBrick.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IScript
		{
			virtual property Windows::Foundation::Collections::IVector<IBrick^>^ Bricks;
		};
	}
}