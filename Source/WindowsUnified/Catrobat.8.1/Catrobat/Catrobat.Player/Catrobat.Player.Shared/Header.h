#pragma once

#include "IHeader.h"

namespace ProjectStructure
{
	class Header
	{
	public:
		Header(Catrobat_Player::NativeComponent::IHeader^ header);
		~Header();

		int GetScreenHeight() { return m_screenHeight; }
		int GetScreenWidth() { return m_screenWidth; }
		std::string GetCatrobatLanguageVersion() { return m_catrobatLanguageVersion; }
		std::string GetApplicationBuildName() { return m_applicationBuildName; }
		int GetApplicationBuildNumber() { return m_applicationBuildNumber; }
		std::string GetApplicationName() { return m_applicationName; }
		std::string GetProgramName() { return m_programName; }
		std::string GetApplicationVersion() { return m_applicationVersion; }
		std::string GetDateTimeUpload() { return m_dateTimeUpload; }
		std::string GetDescription() { return m_description; }
		std::string GetDeviceName() { return m_deviceName; }
		std::string GetMediaLicense() { return m_mediaLicense; }
		std::string GetPlatform() { return m_platform; }
		std::string GetPlatformVersion() { return m_platformVersion; }
		std::string GetProgramLicense() { return m_programLicense; }
		std::string GetRemixOf() { return m_remixOf; }
		std::string GetTags() { return m_tags; }
		std::string GetUrl() { return m_url; }
		std::string GetUserHandle() { return m_userHandle; }

		void SetDefaultScreenSize();

	private:
		std::string m_applicationBuildName;
		int m_applicationBuildNumber;
		std::string m_applicationName;
		std::string m_applicationVersion;
		std::string m_catrobatLanguageVersion;
		std::string m_dateTimeUpload;
		std::string m_description;
		std::string m_deviceName;
		std::string m_mediaLicense;
		std::string m_platform;
		std::string m_platformVersion;
		std::string m_programLicense;
		std::string m_programName;
		std::string m_remixOf;
		int m_screenHeight;
		int m_screenWidth;
		std::string m_tags;
		std::string m_url;
		std::string m_userHandle;
	};
}