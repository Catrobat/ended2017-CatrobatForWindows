#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IGLideToBrick : public IBrick
		{
		public:
			virtual property IFormulaTree^ DestinationX;
			virtual property IFormulaTree^ DestinationY;
			virtual property IFormulaTree^ Duration;
		};
	}
}
