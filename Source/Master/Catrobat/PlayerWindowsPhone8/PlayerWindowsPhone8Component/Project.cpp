#include "pch.h"
#include "Project.h"


Project::Project(int version, int versionCode, string versionName, string projectName, int screenWidth, int screenHeight, SpriteList *spriteList) :
	m_version(version), m_versionCode(versionCode), m_versionName(versionName),
	m_projectName(projectName), m_screenWidth(screenWidth), m_screenHeight(screenHeight), m_spriteList(spriteList)
{
}

int Project::getVersion()
{
	return m_version;
}

int Project::getVersionCode()
{
	return m_versionCode;
}

string Project::getVersionName()
{
	return m_versionName;
}

string Project::getProjectName()
{
	return m_projectName;
}

int Project::getScreenWidth()
{
	return m_screenWidth;
}

int Project::getScreenHeight()
{
	return m_screenHeight;
}

SpriteList *Project::getSpriteList()
{
	return m_spriteList;
}
