#pragma once

#include <string>
#include <list>

#include "Look.h"
#include "Script.h"
#include "SoundInfo.h"
#include "UserVariable.h"
#include "WhenScript.h"

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

    void LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    void Draw(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    void SetupWindowSizeDependentResources(const std::shared_ptr<DX::DeviceResources>& deviceResources);
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

    void SetTranslation(float x, float y);
    void GetTranslation(float &x, float &y);
    Bounds GetBounds();

    void SetTransparency(float transparency);
    float GetTransparency();
    void SetRotation(float rotation);
    float GetRotation();
    void SetScale(float scale);
    float GetScale();

    bool IsTapPointHitting(ID3D11DeviceContext1* context, ID3D11Device* device, double xNormalized, double yNormalized, double resolutionFactor);

    void SetWhenScript(WhenScript* whenScript);
    WhenScript* GetWhenScript();

private:
    Look *m_look;
    list<Look*> *m_lookList;
    list<Script*> *m_scripts;
    list<SoundInfo*> *m_soundInfos;
    std::map<std::string, UserVariable*> *m_variableList;
    string m_name;
    float m_opacity;
    float m_rotation;
    XMFLOAT2 m_origin;
    XMMATRIX m_transformationMatrix;
    D2D1::Matrix3x2F m_transformation;
    float m_ratioX;
    float m_ratioY;
    D2D1_SIZE_F m_renderTargetSize;
    D2D1::Matrix3x2F m_renderTarget;
    WhenScript* m_whenScript;
};
