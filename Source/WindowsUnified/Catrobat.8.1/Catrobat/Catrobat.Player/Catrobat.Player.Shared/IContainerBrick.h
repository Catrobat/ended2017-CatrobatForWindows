#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"
#include "IUserVariable.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IContainerBrick : public IBrick
		{
			virtual property Windows::Foundation::Collections::IVector<IBrick^>^ Bricks;
		};
	}
}
