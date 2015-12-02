#pragma once

#include "Catrobat.PlayerMain.h"
namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IWhenScript
		{
		public:
			//TODO: Maybe enum here?
			virtual property Platform::String^ Action;
		};
	}
}