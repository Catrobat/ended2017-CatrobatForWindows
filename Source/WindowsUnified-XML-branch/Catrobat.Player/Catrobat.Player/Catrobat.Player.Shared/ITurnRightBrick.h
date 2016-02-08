#pragma once

#include "IBrick.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class ITurnRightBrick : public IBrick
		{
		public:
			virtual property int Rotation;
		};
	}
}