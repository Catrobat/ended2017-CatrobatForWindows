#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IPointToBrick : public IBrick
		{
		public:
			virtual property IFormulaTree^ Rotation;
		};
	}
}
