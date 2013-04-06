#include "pch.h"
#include "Sprite.h"


Sprite::Sprite(string name) :
	m_name(name)
{
	m_lookDatas = new list<LookData*>();
	m_scripts = new list<Script*>();
	m_soundInfos = new list<SoundInfo*>();
}

void Sprite::addLookData(LookData *lookData)
{
	m_lookDatas->push_back(lookData);
}

void Sprite::addScript(Script *script)
{
	m_scripts->push_back(script);
}

void Sprite::addSoundInfo(SoundInfo *soundInfo)
{
	m_soundInfos->push_back(soundInfo);
}

string Sprite::getName()
{
	return m_name;
}

int Sprite::ScriptListSize()
{
	return m_scripts->size();
}

int Sprite::LookDataListSize()
{
	return m_lookDatas->size();
}

LookData *Sprite::getLookData(int index)
{
	list<LookData*>::iterator it = m_lookDatas->begin();
	advance(it, index);
	return *it;
}

Script *Sprite::getScript(int index)
{
	list<Script*>::iterator it = m_scripts->begin();
	advance(it, index);
	return *it;
}

void Sprite::Render(SpriteBatch *spriteBatch)
{
	for (int i = 0; i < ScriptListSize(); i++)
	{
		getScript(i)->Render(spriteBatch);
	}
}

void Sprite::LoadTextures(ID3D11Device* d3dDevice)
{
	for (int i = 0; i < ScriptListSize(); i++)
	{
		getScript(i)->LoadTextures(d3dDevice);
	}
}



