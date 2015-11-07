#include "pch.h"
#include "Project.h"
#include "Constants.h"

using namespace std;
using namespace ProjectStructure;
using namespace Catrobat_Player::NativeComponent;


Project::Project(IProject^ project) :
	m_applicationBuildName(Helper::StdString(project->ApplicationBuildName)),
	m_applicationBuildNumber(project->ApplicationBuildNumber),
	m_applicationName(Helper::StdString(project->ApplicationName)),
	m_applicationVersion(Helper::StdString(project->ApplicationVersion)),
	m_catrobatLanguageVersion(Helper::StdString(project->CatrobatLanguageVersion)),
	m_dateTimeUpload(project->DateTimeUpload),
	m_description(Helper::StdString(project->Description)),
	m_deviceName(Helper::StdString(project->DeviceName)),
	m_mediaLicense(Helper::StdString(project->MediaLicense)),
	m_platform(Helper::StdString(project->TargetPlatform)),
	m_platformVersion(project->PlatformVersion),
	m_programLicense(Helper::StdString(project->ProgramLicense)),
	m_programName(Helper::StdString(project->ProgramName)),
	m_remixOf(Helper::StdString(project->RemixOf)),
	m_screenHeight(project->ScreenHeight),
	m_screenWidth(project->ScreenWidth),
	m_url(Helper::StdString(project->Url)),
	m_userHandle(Helper::StdString(project->UserHandle))
{
	for each (Platform::String^ tag in project->Tags)
	{
		m_tags.push_back(Helper::StdString(tag));
	}
	for each (Catrobat_Player::NativeComponent::IObject^ object in project->Objects)
	{
		m_objectList.insert(std::pair<std::string, std::shared_ptr<Object> >(Helper::StdString(object->Name), make_shared<Object>(object)));
	}
}

Project::~Project()
{
}

void Project::CheckProjectScreenSize()
{
	if (m_screenHeight == 0 || m_screenWidth == 0)
	{
		m_screenHeight = Constants::XMLParser::Header::ProjectScreenHeightDefault;
		m_screenWidth = Constants::XMLParser::Header::ProjectScreenWidthDefault;
	}
}

void Project::SetupWindowSizeDependentResources(
	const shared_ptr<DX::DeviceResources>& deviceResources)
{
	for each (pair<string, shared_ptr<Object>> obj in m_objectList)
	{
		obj.second->SetupWindowSizeDependentResources(deviceResources);
	}
}

void Project::LoadTextures(const shared_ptr<DX::DeviceResources>& deviceResources)
{
	for each (pair<string, shared_ptr<Object>> obj in m_objectList)
	{
		obj.second->LoadTextures(deviceResources);
	}
}

void Project::StartUp()
{
	for each (pair<string, shared_ptr<Object>> obj in m_objectList)
	{
		obj.second->StartUp();
	}
}

void Project::Render(const shared_ptr<DX::DeviceResources>& deviceResources)
{
	for each (pair<string, shared_ptr<Object>> obj in m_objectList)
	{
		obj.second->Draw(deviceResources);
	}
}

shared_ptr<UserVariable> Project::GetVariable(string name)
{
	map<string, shared_ptr<UserVariable >>::iterator searchItem = m_variableList.find(name);
	if (searchItem != m_variableList.end())
		return searchItem->second;
	return NULL;
}

