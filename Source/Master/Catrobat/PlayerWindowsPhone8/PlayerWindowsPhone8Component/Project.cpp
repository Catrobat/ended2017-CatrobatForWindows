#include "pch.h"
#include "Project.h"


Project::Project(int androidVersion, int catroidVersionCode, string catroidVersionName, string projectName, int screenWidth, int screenHeight, SpriteList *spriteList) :
	m_androidVersion(androidVersion), m_catroidVersionCode(catroidVersionCode), m_catroidVersionName(catroidVersionName),
	m_projectName(projectName), m_screenWidth(screenWidth), m_screenHeight(screenHeight), m_spriteList(spriteList)
{
}

int Project::getAndroidVersion()
{
	return m_androidVersion;
}

int Project::getCatroidVersionCode()
{
	return m_catroidVersionCode;
}

string Project::getCatroidVersionName()
{
	return m_catroidVersionName;
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
