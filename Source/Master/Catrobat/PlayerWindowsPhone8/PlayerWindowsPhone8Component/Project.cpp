#include "pch.h"
#include "Project.h"

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
	bool								programScreenshotManuallyTaken,
	string								remixOf,
	int									screenHeight,
	int									screenWidth,
	vector<string>*						tags,
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
	m_programScreenshotManuallyTaken	(programScreenshotManuallyTaken),
	m_remixOf							(remixOf),
	m_screenHeight						(screenHeight),
	m_screenWidth						(screenWidth),	
	m_tags								(tags),
	m_url								(url),
	m_userHandle						(userHandle)
{
	m_objectList = new ObjectList();
	m_variableList = new map<string, string>();
}

int Project::ScreenHeight()
{
	return m_screenHeight;
}

int	Project::ScreenWidth()
{
	return m_screenWidth;
}

ObjectList *Project::getObjectList()
{
	return m_objectList;
}

void Project::Render(SpriteBatch *spriteBatch)
{
	for (int i = 0; i < m_objectList->Size(); i++)
	{
		m_objectList->getObject(i)->Draw(spriteBatch);
	}
}

void Project::LoadTextures(ID3D11Device* d3dDevice)
{
	for (int i = 0; i < m_objectList->Size(); i++)
	{
		m_objectList->getObject(i)->LoadTextures(d3dDevice);
	}
}

void Project::StartUp()
{	
	for (int i = 0; i < m_objectList->Size(); i++)
	{
		m_objectList->getObject(i)->StartUp();
	}
}

string Project::Variable(std::string name)
{
	map<string, string>::iterator searchItem = m_variableList->find(name);
	return searchItem->second;
}

void Project::addVariable(std::string name, std::string value)
{
	m_variableList->insert(pair<string, string>(name, value));
}

void Project::addVariable(std::pair<string, string> variable)
{
	m_variableList->insert(variable);
}
