#pragma once

#include "Catrobat.PlayerMain.h"
namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IStartScript
		{
		public:
			virtual property Platform::String^ Name;
		};
	}
}