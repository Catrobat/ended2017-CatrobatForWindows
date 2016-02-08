#include "pch.h"
#include "Header.h"
#include "Helper.h"
#include "Constants.h"

using namespace ProjectStructure;

Header::Header(Catrobat_Player::NativeComponent::IHeader^ header) :
	m_applicationBuildName(Helper::StdString(header->ApplicationBuildName)),
	m_applicationBuildNumber(header->ApplicationBuildNumber),
	m_applicationName(Helper::StdString(header->ApplicationName)),
	m_applicationVersion(Helper::StdString(header->ApplicationVersion)),
	m_catrobatLanguageVersion(Helper::StdString(header->CatrobatLanguageVersion)),
	m_dateTimeUpload(Helper::StdString(header->DateTimeUpload)),
	m_description(Helper::StdString(header->Description)),
	m_deviceName(Helper::StdString(header->DeviceName)),
	m_mediaLicense(Helper::StdString(header->MediaLicense)),
	m_platform(Helper::StdString(header->TargetPlatform)),
	m_platformVersion(Helper::StdString(header->PlatformVersion)),
	m_programLicense(Helper::StdString(header->ProgramLicense)),
	m_programName(Helper::StdString(header->ProgramName)),
	m_remixOf(Helper::StdString(header->RemixOf)),
	m_screenHeight(header->ScreenHeight),
	m_screenWidth(header->ScreenWidth),
	m_tags(Helper::StdString(header->Tags)),
	m_url(Helper::StdString(header->Url)),
	m_userHandle(Helper::StdString(header->UserHandle))
{
}


Header::~Header()
{
}

void Header::SetDefaultScreenSize()
{
	m_screenHeight = Constants::XMLParser::Header::ProjectScreenHeightDefault;
	m_screenWidth = Constants::XMLParser::Header::ProjectScreenWidthDefault;
}