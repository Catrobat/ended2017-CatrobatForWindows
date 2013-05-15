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
	m_spriteList = new SpriteList();
}

int Project::ScreenHeight()
{
	return m_screenHeight;
}

int	Project::ScreenWidth()
{
	return m_screenWidth;
}

SpriteList *Project::getSpriteList()
{
	return m_spriteList;
}

void Project::Render(SpriteBatch *spriteBatch)
{
	for (int i = 0; i < m_spriteList->Size(); i++)
	{
		m_spriteList->getSprite(i)->Draw(spriteBatch);
	}
}

void Project::LoadTextures(ID3D11Device* d3dDevice)
{
	for (int i = 0; i < m_spriteList->Size(); i++)
	{
		m_spriteList->getSprite(i)->LoadTextures(d3dDevice);
	}
}

void Project::StartUp()
{	
	for (int i = 0; i < m_spriteList->Size(); i++)
	{
		m_spriteList->getSprite(i)->StartUp();
	}
}
