#pragma once

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