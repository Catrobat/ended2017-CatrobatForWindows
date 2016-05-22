#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IBroadcastBrick : public IBrick
		{
		public:
			virtual property Platform::String^ BroadcastMessage;
		};
	}
}
