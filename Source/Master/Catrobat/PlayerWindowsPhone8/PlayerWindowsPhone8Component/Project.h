#pragma once

#include "BaseObject.h"
#include "SpriteList.h"

class Project
{
public:
	Project(int androidVersion, int catroidVersionCode, string CatroidVersionName, string projectName, 
		int screenWidth, int screenHeight);
	~Project();

	int getAndroidVersion();
	int getCatroidVersionCode();
	string getCatroidVersionName();
	string getProjectName();
	int getScreenWidth();
	int getScreenHeight();
	SpriteList *getSpriteList();

	void Render(SpriteBatch *spriteBatch);
	void LoadTextures(ID3D11Device* d3dDevice);
	void StartUp();

private:
	int m_androidVersion;
	int m_catroidVersionCode;
	string m_catroidVersionName;
	string m_projectName;
	int m_screenWidth;
	int m_screenHeight;
	SpriteList *m_spriteList;
};

