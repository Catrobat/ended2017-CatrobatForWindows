#pragma once

#include "IScript.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IBroadcastScript :IScript
		{
		public:
			virtual property Platform::String^ Name;
			virtual property Platform::String^ ReceivedMessage;
		};
	}
}