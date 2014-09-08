#pragma once

#include <string>
#include <list>

#include "Look.h"
#include "Script.h"
#include "SoundInfo.h"
#include "UserVariable.h"
#include "WhenScript.h"

using namespace std;

class Object
{
public:
    Object(string name);

public:
    void SetTranslation(float x, float y);
    void GetTranslation(float &x, float &y);

    void SetTransparency(float transparency);
    float GetTransparency();

    void SetRotation(float rotation);
    float GetRotation();

    void SetScale(float x, float y);
    void GetScale(float &x, float &y);

public:
    void AddLook(Look *lookData);
    void AddScript(Script *script);
    void AddSoundInfo(SoundInfo *soundInfo);
    void AddVariable(std::string name, UserVariable *variable);
    void AddVariable(std::pair<std::string, UserVariable*> variable);
    void SetLook(int index);
    void SetWhenScript(WhenScript* whenScript);

public:
    void LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    void SetupWindowSizeDependentResources(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    void StartUp();
    void Draw(const std::shared_ptr<DX::DeviceResources>& deviceResources);

public:
    string GetName();

    int GetScriptListSize();
    Script *GetScript(int index);

    UserVariable *GetVariable(std::string name);

    int GetLookDataListSize();
    Look *GetLook(int index);
    int GetLook();
    int GetLookCount();
    Look* GetCurrentLook();
    WhenScript* GetWhenScript();

private:
    string m_name;

    Look *m_look;
    list<Look*> *m_lookList;
    list<Script*> *m_scripts;
    list<SoundInfo*> *m_soundInfos;
    std::map<std::string, UserVariable*> *m_variableList;
    WhenScript* m_whenScript;

private:
    float m_opacity;
    float m_rotation;
    D2D1_POINT_2F m_translation;
    D2D1_SIZE_F m_objectScale;
    D2D1::Matrix3x2F m_transformation;
    D2D1_SIZE_F m_ratio;
    D2D1_SIZE_F m_logicalSize;
    D2D1_SIZE_F m_renderTargetSize;

private:
    void RecalculateTransformation();
    static double Radians(float degree) { return degree * XM_PI / 180.0f; }
};
