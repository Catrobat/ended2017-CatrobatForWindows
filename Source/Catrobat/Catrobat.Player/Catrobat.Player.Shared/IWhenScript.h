#pragma once

#include "IScript.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IWhenScript : public IScript
		{
		public:
			//TODO: Maybe enum here?
			virtual property Platform::String^ Action;
		};
	}
}