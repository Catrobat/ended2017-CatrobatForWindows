#pragma once

#include "ILook.h"
#include "IUserVariable.h"
#include "IStartScript.h"
#include "IWhenScript.h"
#include "IBroadcastScript.h"

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IObject
		{
		public:
			virtual property Platform::String^ Name;
			virtual property Windows::Foundation::Collections::IVector<ILook^>^ Looks;
			virtual property Windows::Foundation::Collections::IVector<IStartScript^>^ StartScripts;
			virtual property Windows::Foundation::Collections::IVector<IWhenScript^>^ WhenScripts;
			virtual property Windows::Foundation::Collections::IVector<IBroadcastScript^>^ BroadcastScripts;
			virtual property Windows::Foundation::Collections::IVector<IUserVariable^>^ UserVariables;
		};
	}
}