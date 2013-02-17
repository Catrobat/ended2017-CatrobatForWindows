#pragma once

#include <string>
#include <list>

#include "LookData.h"
#include "Script.h"
#include "SoundInfo.h"

using namespace std;

class Sprite
{
public:
	Sprite(string name);
	~Sprite();

	void addLookData(LookData *lookData);
	void addScript(Script *script);
	void addSoundInfo(SoundInfo *soundInfo);

private:
	list<LookData*> *m_lookDatas;
	list<Script*> *m_scripts;
	list<SoundInfo*> *m_soundInfos;
	string m_name;
};

