#include "pch.h"
#include "Project.h"
#include "Constants.h"

using namespace std;

//--------------------------------------------------------------------------------------------------

Project::Project(
	string								applicationBuildName,
	int									applicationBuildNumber,
	string								applicationName,
	string								applicationVersion,
	string								catrobatLanguageVersion,
	time_t								dateTimeUpload,
	string								description,
	string								deviceName,
	string								mediaLicense,
	string								platform,
	int									platformVersion,
	string								programLicense,
	string								programName,
	string								remixOf,
	int									screenHeight,
	int									screenWidth,
	vector<string>						tags,
	string								url,
	string								userHandle
	) :
	m_applicationBuildName				(applicationBuildName),
	m_applicationBuildNumber			(applicationBuildNumber),
	m_applicationName					(applicationName),
	m_applicationVersion				(applicationVersion),
	m_catrobatLanguageVersion			(catrobatLanguageVersion),
	m_dateTimeUpload					(dateTimeUpload),
	m_description						(description),
	m_deviceName						(deviceName),
	m_mediaLicense						(mediaLicense),
	m_platform							(platform),
	m_platformVersion					(platformVersion),
	m_programLicense					(programLicense),
	m_programName						(programName),
	m_remixOf							(remixOf),
	m_screenHeight						(screenHeight),
	m_screenWidth						(screenWidth),	
	m_tags								(tags),
	m_url								(url),
	m_userHandle						(userHandle)
{
}

Project::Project()
{
	m_screenWidth = 600;
	m_screenHeight = 800;
}

Project::~Project()
{
}

//--------------------------------------------------------------------------------------------------

int Project::GetScreenHeight()
{
	return m_screenHeight;
}

//--------------------------------------------------------------------------------------------------

int	Project::GetScreenWidth()
{
	return m_screenWidth;
}

//--------------------------------------------------------------------------------------------------

std::map<std::string, std::shared_ptr<Object> >	 Project::GetObjectList()
{
	return m_objectList;
}

//--------------------------------------------------------------------------------------------------

void Project::Render(const shared_ptr<DX::DeviceResources>& deviceResources)
{
    for each (pair<string, shared_ptr<Object>> obj in m_objectList)
    {
        obj.second->Draw(deviceResources);
    }
}

//--------------------------------------------------------------------------------------------------

void Project::LoadTextures(const shared_ptr<DX::DeviceResources>& deviceResources)
{
    for each (pair<string, shared_ptr<Object>> obj in m_objectList)
    {
        obj.second->LoadTextures(deviceResources);
    }
}

//--------------------------------------------------------------------------------------------------

void Project::SetupWindowSizeDependentResources(
    const shared_ptr<DX::DeviceResources>& deviceResources)
{
    for each (pair<string, shared_ptr<Object>> obj in m_objectList)
    {
        obj.second->SetupWindowSizeDependentResources(deviceResources);
    }
}

//--------------------------------------------------------------------------------------------------

void Project::CheckProjectScreenSize()
{
    if (m_screenHeight == 0 || m_screenWidth == 0)
    {
        m_screenHeight = Constants::XMLParser::Header::ProjectScreenHeightDefault;
        m_screenWidth = Constants::XMLParser::Header::ProjectScreenWidthDefault;
    }
}

//--------------------------------------------------------------------------------------------------

void Project::StartUp()
{	
    for each (pair<string, shared_ptr<Object>> obj in m_objectList)
    {
        obj.second->StartUp();
    }
}

//--------------------------------------------------------------------------------------------------

shared_ptr<UserVariable> Project::GetVariable(string name)
{
	map<string, shared_ptr<UserVariable >>::iterator searchItem = m_variableList.find(name);
	if (searchItem != m_variableList.end())
		return searchItem->second;
	return NULL;
}

//--------------------------------------------------------------------------------------------------

void Project::AddVariable(std::string name, shared_ptr<UserVariable> variable)
{
	m_variableList.insert(pair<string,shared_ptr<UserVariable> >(name, variable));
}

//--------------------------------------------------------------------------------------------------

void Project::AddVariable(std::pair<string, shared_ptr<UserVariable> > variable)
{
	m_variableList.insert(variable);
}

//--------------------------------------------------------------------------------------------------

void Project::AddObject(std::pair<string, shared_ptr<Object> > object)
{
	m_objectList.insert(object);
}


std::string Project::GetProgramName() 
{ 
	return m_programName; 
}


