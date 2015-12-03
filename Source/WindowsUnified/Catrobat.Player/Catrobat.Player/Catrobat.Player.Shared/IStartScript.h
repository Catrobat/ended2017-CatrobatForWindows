#pragma once

#include "IScript.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IStartScript : public IScript
		{
		public:
			virtual property Platform::String^ Name;
		};
	}
}