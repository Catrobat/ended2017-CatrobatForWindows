#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IIfBrick : public IBrick
		{
		public:
			virtual property Windows::Foundation::Collections::IVector<IBrick^>^ IfBricks;
			virtual property Windows::Foundation::Collections::IVector<IBrick^>^ ElseBricks;
			virtual property IFormulaTree^ Condition;
			virtual property Platform::String^ CurrentAddMode;
		};
	}
}
