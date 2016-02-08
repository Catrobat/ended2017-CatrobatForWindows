#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class ICostumeBrick : public IBrick
		{
		public:
			virtual property Platform::String^ CostumeDataReference;
			virtual property int Index;
		};
	}
}
