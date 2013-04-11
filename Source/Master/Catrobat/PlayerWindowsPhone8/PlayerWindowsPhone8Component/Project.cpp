#include "pch.h"
#include "Project.h"


Project::Project(int androidVersion, int catroidVersionCode, string catroidVersionName, string projectName, int screenWidth, int screenHeight) :
	m_androidVersion(androidVersion), m_catroidVersionCode(catroidVersionCode), m_catroidVersionName(catroidVersionName),
	m_projectName(projectName), m_screenWidth(screenWidth), m_screenHeight(screenHeight)
{
	m_spriteList = new SpriteList();
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
