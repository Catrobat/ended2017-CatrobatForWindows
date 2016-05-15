#pragma once

#include "IBrick.h"
#include "IFormulaTree.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IPlaySoundBrick : public IBrick
		{
		public:
			virtual property Platform::String^ FileName;
			virtual property Platform::String^ Name;
		};
	}
}
