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

int Sprite::Size()
{
	return m_scripts->size();
}

Script *Sprite::getScript(int index)
{
	list<Script*>::iterator it = m_scripts->begin();
	advance(it, index);
	return *it;
}



