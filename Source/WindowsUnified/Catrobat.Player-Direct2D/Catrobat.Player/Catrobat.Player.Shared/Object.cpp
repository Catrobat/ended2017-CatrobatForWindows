#include "pch.h"
#include "Object.h"
#include "ProjectDaemon.h"
#include "PlayerException.h"

#include <exception>
#include <math.h>

using namespace D2D1;

Object::Object(string name) :
m_name(name),
m_look(NULL),
m_opacity(1.f),
m_rotation(0.f),
m_translation(Point2F()),
m_objectScale(SizeF(1.f, 1.f)),
m_transformation(Matrix3x2F()),
m_ratio(SizeF()),
m_logicalSize(SizeF()),
m_renderTargetSize(SizeF())
{
    m_lookList = new list<Look*>();
    m_scripts = new list<Script*>();
    m_soundInfos = new list<SoundInfo*>();
    m_variableList = new map<string, UserVariable*>();
}

#pragma region TRANSFORMATION
void Object::SetTranslation(float x, float y)
{
    m_translation = Point2F(x, y);
    RecalculateTransformation();
}

void Object::GetTranslation(float &x, float &y)
{
    x = m_translation.x;
    y = m_translation.y;
}

void Object::SetTransparency(float transparency)
{
    if ((1.0f - transparency) > 0)
    {
        if ((1.0f - transparency) < 1.0f)
        {
            m_opacity = 1.0f - transparency;
        }
        else
        {
            m_opacity = 1.0f;
        }
    }
    else
    {
        m_opacity = 0.0f;
    }
}

float Object::GetTransparency()
{
    return 1.0f - m_opacity;
}

void Object::SetRotation(float rotation)
{
    m_rotation = rotation;
    RecalculateTransformation();
}

float Object::GetRotation()
{
    return m_rotation;
}

void Object::SetScale(float x, float y)
{
    if (x < 0.f)
    {
        x = 0.f;
    }
    if (y < 0.f)
    {
        y = 0.f;
    }

    m_objectScale = SizeF(x, y);
    RecalculateTransformation();
}

void Object::GetScale(float &x, float &y)
{
    x = m_objectScale.width;
    y = m_objectScale.height;
}
#pragma endregion

#pragma region GENERAL
void Object::AddLook(Look *lookData)
{
    m_lookList->push_back(lookData);
}

void Object::AddScript(Script *script)
{
    m_scripts->push_back(script);
}

void Object::AddSoundInfo(SoundInfo *soundInfo)
{
    m_soundInfos->push_back(soundInfo);
}
void Object::AddVariable(string name, UserVariable* variable)
{
    m_variableList->insert(pair<string, UserVariable*>(name, variable));
}

void Object::AddVariable(pair<string, UserVariable*> variable)
{
    m_variableList->insert(variable);
}

void Object::SetLook(int index)
{
    m_look = GetLook(index);
    RecalculateTransformation();
}

void Object::SetWhenScript(WhenScript* whenScript)
{
    m_whenScript = whenScript;
}
#pragma endregion

#pragma region RENDERING
void Object::LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources)
{
    SetTranslation(0.f, 0.f);

    for (int i = 0; i < GetLookDataListSize(); i++)
    {
        m_look = GetLook(i);

        if (m_look != NULL)
        {
            m_look->LoadTexture(deviceResources);
        }
    }
}

void Object::SetupWindowSizeDependentResources(const std::shared_ptr<DX::DeviceResources>& deviceResources)
{
    auto deviceContext = deviceResources->GetD2DDeviceContext();
    m_logicalSize = deviceContext->GetSize();
    m_ratio.width = m_logicalSize.width / ProjectDaemon::Instance()->GetProject()->GetScreenWidth();
    m_ratio.height = m_logicalSize.height / ProjectDaemon::Instance()->GetProject()->GetScreenHeight() * (-1);
    RecalculateTransformation();
}

void Object::StartUp()
{
    if (m_lookList != NULL && m_lookList->size() > 0)
    {
        m_look = m_lookList->front();
    }

    for (int i = 0; i < GetScriptListSize(); i++)
    {
        Script *script = GetScript(i);

        if (script->GetType() == Script::TypeOfScript::StartScript)
        {
            script->Execute();
        }
    }
}

void Object::Draw(const std::shared_ptr<DX::DeviceResources>& deviceResources)
{
    if (m_look == NULL)
    {
        return;
    }

    auto deviceContext = deviceResources->GetD2DDeviceContext();
    deviceContext->SetTransform(m_transformation);
    deviceContext->Clear(ColorF(ColorF::White));
    deviceContext->DrawBitmap(m_look->GetBitMap(),
        RectF(0.f, 0.f, m_renderTargetSize.width, m_renderTargetSize.height));
}
#pragma endregion

#pragma region GETTERS
string Object::GetName()
{
    return m_name;
}

int Object::GetScriptListSize()
{
    return m_scripts->size();
}

Script *Object::GetScript(int index)
{
    list<Script*>::iterator it = m_scripts->begin();
    advance(it, index);
    return *it;
}

UserVariable* Object::GetVariable(string name)
{
    map<string, UserVariable*>::iterator searchItem = m_variableList->find(name);

    if (searchItem != m_variableList->end())
    {
        return searchItem->second;
    }

    return NULL;
}

int Object::GetLookDataListSize()
{
    return m_lookList->size();
}

Look *Object::GetLook(int index)
{
    list<Look*>::iterator it = m_lookList->begin();
    advance(it, index);

    if (it != m_lookList->end())
    {
        return *it;
    }

    return NULL;
}

int Object::GetLook()
{
    int i = 0;
    list<Look*>::iterator it = m_lookList->begin();

    while ((*it) != m_look)
    {
        it++;
        i++;
    }

    if (it != m_lookList->end())
    {
        return i;
    }

    return 0;
}

int Object::GetLookCount()
{
    return m_lookList->size();
}

Look* Object::GetCurrentLook()
{
    if (!m_look)
    {
        return GetLook(0);
    }

    return m_look;
}

WhenScript* Object::GetWhenScript()
{
    return m_whenScript;
}

#pragma endregion

#pragma region INTERNAL
void Object::RecalculateTransformation()
{
    if (m_look == NULL)
    {
        return;
    }
    m_renderTargetSize = m_look->GetBitMap()->GetSize();
    m_renderTargetSize.width *= m_ratio.width;
    m_renderTargetSize.height *= m_ratio.height;
    Matrix3x2F renderTarget = Matrix3x2F::Identity();
    renderTarget = Matrix3x2F::Translation(m_logicalSize.width / 2 - m_renderTargetSize.width / 2,
        m_logicalSize.height / 2 - m_renderTargetSize.height / 2);

    Matrix3x2F translation = Matrix3x2F::Translation(m_translation.x * m_ratio.width, m_translation.y * m_ratio.height) * renderTarget;
    D2D1_POINT_2F origin;
    origin.x = translation._31 + m_renderTargetSize.width / 2;
    origin.y = translation._32 + m_renderTargetSize.height / 2;

    m_transformation = translation *
        Matrix3x2F::Rotation(m_rotation, origin) *
        Matrix3x2F::Scale(m_objectScale, origin);
}
#pragma endregion