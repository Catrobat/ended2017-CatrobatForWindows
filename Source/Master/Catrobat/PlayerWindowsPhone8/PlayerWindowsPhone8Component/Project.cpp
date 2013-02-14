#include "pch.h"
#include "Project.h"


Project::Project(int version, int versionCode, int versionName, string projectName, int screenWidth, int screenHeight, SpriteList *spriteList) :
	m_version(version), m_versionCode(versionCode), m_versionName(versionName),
	m_projectName(projectName), m_screenWidth(screenWidth), m_screenHeight(screenHeight), m_spriteList(spriteList)
{
}

