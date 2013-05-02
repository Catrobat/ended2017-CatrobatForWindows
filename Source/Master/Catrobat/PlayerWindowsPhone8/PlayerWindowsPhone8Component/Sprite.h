#pragma once

#include <string>
#include <list>

#include "LookData.h"
#include "Script.h"
#include "SoundInfo.h"

using namespace std;

struct Bounds
{
    float x;
    float y;
	float width;
	float height;
};

class Sprite : BaseObject
{
public:
	Sprite(string name);
	~Sprite();

	void addLookData(LookData *lookData);
	void addScript(Script *script);
	void addSoundInfo(SoundInfo *soundInfo);

	void LoadTextures(ID3D11Device* d3dDevice, Windows::Foundation::Rect *windowBounds);
	void Draw(SpriteBatch *spriteBatch);
	void StartUp();

	int ScriptListSize();
	Script *getScript(int index);
	string getName();

	int LookDataListSize();
	LookData *getLookData(int index);
	void SetLookData(int index);
	LookData* GetCurrentLookData();

	void SetPosition(float x, float y);

	Bounds getBounds();

private:
	LookData *m_lookData;
	list<LookData*> *m_lookDatas;
	list<Script*> *m_scripts;
	list<SoundInfo*> *m_soundInfos;
	string m_name;
};
