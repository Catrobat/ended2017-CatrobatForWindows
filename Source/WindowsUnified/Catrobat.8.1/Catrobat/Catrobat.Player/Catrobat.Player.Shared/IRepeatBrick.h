#pragma once

#include "IContainerBrick.h"
#include "IFormulaTree.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IRepeatBrick : public IContainerBrick
		{
		public:
			virtual property IFormulaTree^ TimesToRepeat;
		};
	}
}
