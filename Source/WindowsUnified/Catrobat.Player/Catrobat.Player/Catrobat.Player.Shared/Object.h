#pragma once

#include <string>
#include <list>
#include <D3D11.h>
#include <windows.foundation.h>

#include "Common\DeviceResources.h"
#include "Look.h"
#include "SoundInfo.h"
#include "UserVariable.h"
#include "WhenScript.h"

class Script;
class Object
{
public:
    Object(std::string name);
    // Constructor for Objects to store initial values
    Object();

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
    void AddLook(std::shared_ptr<Look> lookData);
    void AddScript(std::shared_ptr<Script> script);
    void AddSoundInfo(SoundInfo *soundInfo);
    void AddVariable(std::pair<std::string, std::shared_ptr<UserVariable> > variable);
    void SetLook(int index);

public:
    void LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    void SetupWindowSizeDependentResources(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    void StartUp();
    void Draw(const std::shared_ptr<DX::DeviceResources>& deviceResources);

public:
    std::string GetName();
    std::list<SoundInfo*>* GetSoundInfos() { return m_soundInfos; };

    int GetScriptListSize();
    std::shared_ptr<Script> GetScript(int index);

    std::shared_ptr<UserVariable> GetVariable(std::string name);

    int GetLookListSize();
    int GetIndexOfCurrentLook();

	std::shared_ptr<Look> GetCurrentLook() { return m_look; };

    bool IsObjectHit(D2D1_POINT_2F position);

private:
    std::string m_name;

    std::shared_ptr<Look> m_look;
    std::list<std::shared_ptr<Look> > m_lookList;
    std::list<std::shared_ptr<Script> > m_scripts;
    std::list<SoundInfo*> *m_soundInfos;
    std::map<std::string, std::shared_ptr<UserVariable> > m_variableList;

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
    D2D1::Matrix3x2F CalculateTranslationMatrix();
    static double Radians(float degree) { return degree * DirectX::XM_PI / 180.0f; }
};
