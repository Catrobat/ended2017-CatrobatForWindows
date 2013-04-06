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

	void Render(SpriteBatch *spriteBatch);
	void LoadTextures(ID3D11Device* d3dDevice);

	int ScriptListSize();
	Script *getScript(int index);
	string getName();

	int LookDataListSize();
	LookData *getLookData(int index);

private:
	list<LookData*> *m_lookDatas;
	list<Script*> *m_scripts;
	list<SoundInfo*> *m_soundInfos;
	string m_name;
};

