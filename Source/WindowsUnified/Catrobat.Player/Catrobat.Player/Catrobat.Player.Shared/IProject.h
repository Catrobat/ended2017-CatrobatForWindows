#pragma once

#include "collection.h"

#include "Catrobat.PlayerMain.h"
#include "IObject.h"
#include "IHeader.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IProject
		{
		public:
			virtual property IHeader^ Header;
			virtual property Windows::Foundation::Collections::IVector<IObject^>^ Variables;
			virtual property Windows::Foundation::Collections::IVector<IObject^>^ Objects;
		};
	}
}