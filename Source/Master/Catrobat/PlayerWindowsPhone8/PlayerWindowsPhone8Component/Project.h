#pragma once

#include "BaseObject.h"
#include "SpriteList.h"

class Project
{
public:
	Project(int version, int versionCode, string versionName, string projectName, 
		int screenWidth, int screenHeight, SpriteList *spriteList);
	~Project();

	int getVersion();
	int getVersionCode();
	string getVersionName();
	string getProjectName();
	int getScreenWidth();
	int getScreenHeight();
	SpriteList *getSpriteList();

private:
	int m_version;
	int m_versionCode;
	string m_versionName;
	string m_projectName;
	int m_screenWidth;
	int m_screenHeight;
	SpriteList *m_spriteList;
};

