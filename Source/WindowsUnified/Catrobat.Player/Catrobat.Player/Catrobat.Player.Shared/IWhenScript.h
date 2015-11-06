#pragma once

#include "Catrobat.PlayerMain.h"
namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IWhenScript
		{
		public:
			virtual property Platform::String^ Name;
			// TODO: Maybe enum here?
			virtual property Platform::String^ Action;
		};
	}
}