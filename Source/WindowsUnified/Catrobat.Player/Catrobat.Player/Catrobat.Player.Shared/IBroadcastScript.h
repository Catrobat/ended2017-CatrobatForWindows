#pragma once

#include "Catrobat.PlayerMain.h"
namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IBroadcastScript
		{
		public:
			virtual property Platform::String^ Name;
			virtual property Platform::String^ ReceivedMessage;
		};
	}
}