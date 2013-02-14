#pragma once

#include "BaseObject.h"
#include "SpriteList.h"

class Project
{
public:
	Project(int version, int versionCode, int versionName, string projectName, 
		int screenWidth, int screenHeight, SpriteList *spriteList);
	~Project();

private:
	int m_version;
	int m_versionCode;
	int m_versionName;
	string m_projectName;
	int m_screenWidth;
	int m_screenHeight;
	SpriteList *m_spriteList;
};

