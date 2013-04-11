#pragma once

#include <string>
#include <list>

#include "LookData.h"
#include "Script.h"
#include "SoundInfo.h"

using namespace std;

class Sprite : BaseObject
{
public:
	Sprite(string name);
	~Sprite();

	void addLookData(LookData *lookData);
	void addScript(Script *script);
	void addSoundInfo(SoundInfo *soundInfo);

	void LoadTextures(ID3D11Device* d3dDevice);
	void Draw(SpriteBatch *spriteBatch);
	void StartUp();

	int ScriptListSize();
	Script *getScript(int index);
	string getName();

	int LookDataListSize();
	LookData *getLookData(int index);
	void SetLookData(int index);

private:
	LookData *m_lookData;
	list<LookData*> *m_lookDatas;
	list<Script*> *m_scripts;
	list<SoundInfo*> *m_soundInfos;
	string m_name;
};
