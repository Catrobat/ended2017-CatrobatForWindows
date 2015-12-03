#pragma once

#include "IScript.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IWhenScript : IScript
		{
		public:
			//TODO: Maybe enum here?
			virtual property Platform::String^ Action;
		};
	}
}