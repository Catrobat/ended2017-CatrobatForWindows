#pragma once

#include <string>
#include <list>

#include "Look.h"
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

class Object : BaseObject
{
public:
	Object(string name);
	~Object();

	void addLook(Look *lookData);
	void addScript(Script *script);
	void addSoundInfo(SoundInfo *soundInfo);

	void LoadTextures(ID3D11Device* d3dDevice);
	void Draw(SpriteBatch *spriteBatch);
	void StartUp();

	int ScriptListSize();
	Script *getScript(int index);
	void addVariable(std::string name, std::string value);
	void addVariable(std::pair<std::string, std::string> variable);
	std::string Variable(std::string name);
	string getName();

	int LookDataListSize();
	Look *getLook(int index);
	void SetLook(int index);
	int GetLook();
	int GetLookCount();
	Look* GetCurrentLook();

	void SetPosition(float x, float y);
	void GetPosition(float &x, float &y);

	Bounds getBounds();

	void SetTransparency(float transparency);
	float GetTransparency();
	void SetRotation(float rotation);
	float GetRotation();
	void SetScale(float scale);
	float GetScale();

private:
	Look *m_look;
	list<Look*> *m_lookList;
	list<Script*> *m_scripts;
	list<SoundInfo*> *m_soundInfos;
	std::map<std::string, std::string> *m_variableList;
	string m_name;
	float m_opacity;
	float m_rotation;
};
