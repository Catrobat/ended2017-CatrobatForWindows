#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IForeverBrick : public IBrick
		{
		public:
			virtual property Windows::Foundation::Collections::IVector<IBrick^>^ Bricks;
		};
	}
}
