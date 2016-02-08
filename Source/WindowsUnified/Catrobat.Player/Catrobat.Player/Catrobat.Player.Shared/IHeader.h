#pragma once

namespace Catrobat_Player
{
	namespace NativeComponent
	{
		public interface class IHeader
		{
		public:
			virtual property Platform::String^ Name;
			virtual property int ApplicationBuildNumber;
			virtual property Platform::String^ ApplicationBuildName;
			virtual property Platform::String^ ApplicationName;
			virtual property Platform::String^ ApplicationVersion;
			virtual property Platform::String^ CatrobatLanguageVersion;
			virtual property time_t DateTimeUpload;
			virtual property Platform::String^ Description;
			virtual property Platform::String^ DeviceName;
			virtual property Platform::String^ MediaLicense;
			virtual property Platform::String^ TargetPlatform;
			virtual property int PlatformVersion;
			virtual property Platform::String^ ProgramLicense;
			virtual property Platform::String^ ProgramName;
			virtual property Platform::String^ RemixOf;
			virtual property int ScreenHeight;
			virtual property int ScreenWidth;
			virtual property Windows::Foundation::Collections::IVector<Platform::String^>^ Tags;
			virtual property Platform::String^ Url;
			virtual property Platform::String^ UserHandle;
		};
	}
}