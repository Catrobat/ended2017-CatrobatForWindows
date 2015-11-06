#pragma once

#include "Catrobat.PlayerMain.h"
namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IUserVariable
		{
		public:
			virtual property Platform::String^ Name;
		};
	}
}