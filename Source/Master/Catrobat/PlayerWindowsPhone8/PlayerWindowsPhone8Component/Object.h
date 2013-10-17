#pragma once

#include <string>
#include <list>

#include "Look.h"
#include "Script.h"
#include "SoundInfo.h"
#include "UserVariable.h"
#include "Interpreter.h"
#include "RotationProvider.h"

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

	void AddLook(Look *lookData);
	void AddScript(Script *script);
	void AddSoundInfo(SoundInfo *soundInfo);

	void LoadTextures(ID3D11Device* d3dDevice);
	void Draw(SpriteBatch *spriteBatch);
	void StartUp();

	int GetScriptListSize();
	Script *GetScript(int index);
	void AddVariable(std::string name, UserVariable *variable);
	void AddVariable(std::pair<std::string, UserVariable*> variable);
	UserVariable *GetVariable(std::string name);
	string GetName();

	int GetLookDataListSize();
	Look *GetLook(int index);
	void SetLook(int index);
	int GetLook();
	int GetLookCount();
	Look* GetCurrentLook();

	void SetPosition(float x, float y);
	void GetPosition(float &x, float &y);

	Bounds GetBounds();

	void SetTransparency(float transparency);
	float GetTransparency();

    void SetRotation(RotationProvider* rotation);
	float GetRotation();
    RotationProvider* GetRotationProvider();

	void SetScale(float scale);
	float GetScale();

private:
	Look *m_look;
	list<Look*> *m_lookList;
	list<Script*> *m_scripts;
	list<SoundInfo*> *m_soundInfos;
	std::map<std::string, UserVariable*> *m_variableList;
	string m_name;
	float m_opacity;
    RotationProvider* m_rotation;
};
