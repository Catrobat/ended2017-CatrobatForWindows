#include "pch.h"
#include "Object.h"
#include "ProjectDaemon.h"
#include "PlayerException.h"
#include "OutOfBoundsException.h"

#include <exception>
#include <math.h>

using namespace D2D1;
using namespace std;

Object::Object(std::string name) :
m_name(name),
m_look(nullptr),
m_opacity(1.f),
m_rotation(0.f),
m_translation(Point2F()),
m_objectScale(SizeF(1.f, 1.f)),
m_transformation(Matrix3x2F()),
m_ratio(SizeF()),
m_logicalSize(SizeF()),
m_renderTargetSize(SizeF())
{
    m_soundInfos = new list<SoundInfo*>();
    //m_variableList(map<string, shared_ptr<UserVariable> >());
}

Object::Object()
{
    m_soundInfos = new std::list<SoundInfo*>();
    //m_variableList(map<string, shared_ptr<UserVariable> >());
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
void Object::AddLook(shared_ptr<Look> lookData)
{
    m_lookList.push_back(lookData);
}

void Object::AddScript(shared_ptr<Script> script)
{
    m_scripts.push_back(script);
}

void Object::AddSoundInfo(SoundInfo *soundInfo)
{
    m_soundInfos->push_back(soundInfo);
}

void Object::AddVariable(std::pair<std::string, shared_ptr<UserVariable> > variable)
{
    m_variableList.insert(variable);
}

void Object::SetLook(int index)
{
    if (m_lookList.size() > index)
    {
        list<shared_ptr<Look>>::iterator it = m_lookList.begin();
        advance(it, index);
        m_look = *it;
        RecalculateTransformation();
    }
}
#pragma endregion

#pragma region RENDERING
void Object::LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources)
{
    SetTranslation(0.f, 0.f);

    for (auto const& look : m_lookList)
    {
        look->LoadTexture(deviceResources);
    }
}

void Object::SetupWindowSizeDependentResources(const std::shared_ptr<DX::DeviceResources>& deviceResources)
{
    auto deviceContext = deviceResources->GetD2DDeviceContext();
    m_logicalSize = deviceContext->GetSize();

	int screen_width = ProjectDaemon::Instance()->GetProject()->GetScreenWidth();
	int screen_height = ProjectDaemon::Instance()->GetProject()->GetScreenHeight();

    m_ratio.width = m_logicalSize.width / ProjectDaemon::Instance()->GetProject()->GetScreenWidth();
    m_ratio.height = m_logicalSize.height / ProjectDaemon::Instance()->GetProject()->GetScreenHeight() * (-1);
    RecalculateTransformation();
}

void Object::StartUp()
{
    SetLook(0);

    for (auto const& script : m_scripts)
    {
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
    deviceContext->DrawBitmap(m_look->GetBitMap().Get(),
        RectF(0.f, 0.f, m_renderTargetSize.width, m_renderTargetSize.height), m_opacity);

}
#pragma endregion

#pragma region GETTERS
std::string Object::GetName()
{
    return m_name;
}

int Object::GetScriptListSize()
{
    return m_scripts.size();
}

shared_ptr<UserVariable> Object::GetVariable(std::string name)
{
    map<string, shared_ptr<UserVariable> >::iterator searchItem = m_variableList.find(name);

    if (searchItem != m_variableList.end())
    {
        return searchItem->second;
    }

    return NULL;
}

int Object::GetLookListSize()
{
    return m_lookList.size();
}

int Object::GetIndexOfCurrentLook()
{
    list<shared_ptr<Look>>::iterator it = m_lookList.begin();
    if (it == m_lookList.end())
    {
        throw new PlayerException("LookList empty! No current look available.");
    }
    while ((*it) != m_look)
    {
        it++;
    }
    return distance(m_lookList.begin(), it);
}

shared_ptr<Script> Object::GetScript(int index)
{
    if (m_scripts.size() > index)
    {
        list<shared_ptr<Script>>::iterator it = m_scripts.begin();
        advance(it, index);
        return *it;
    }
    return nullptr;
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

    Matrix3x2F translation = CalculateTranslationMatrix();
    D2D1_POINT_2F origin;
    origin.x = translation._31 + m_renderTargetSize.width / 2;
    origin.y = translation._32 + m_renderTargetSize.height / 2;

    m_transformation = translation *
        Matrix3x2F::Rotation(m_rotation, origin) *
        Matrix3x2F::Scale(m_objectScale, origin);
}

Matrix3x2F Object::CalculateTranslationMatrix()
{
    m_renderTargetSize = m_look->GetBitMap()->GetSize();
    m_renderTargetSize.width *= m_ratio.width;
    m_renderTargetSize.height *= m_ratio.height;
    Matrix3x2F renderTarget = Matrix3x2F::Identity();
    renderTarget = Matrix3x2F::Translation(m_logicalSize.width / 2 - m_renderTargetSize.width / 2,
        m_logicalSize.height / 2 - m_renderTargetSize.height / 2);

    return Matrix3x2F::Translation(m_translation.x * m_ratio.width, m_translation.y * m_ratio.height) * renderTarget;
}
#pragma endregion

bool Object::IsObjectHit(D2D1_POINT_2F position)
{
    if (m_look == NULL)
    {
        return false;
    }

    Matrix3x2F translation = CalculateTranslationMatrix();
    D2D1_POINT_2F origin;
    origin.x = (float) m_look->GetWidth() / 2;
    origin.y = (float) m_look->GetHeight() / 2;

    Matrix3x2F positionInBitMap = translation;
    positionInBitMap._31 = (position.x - positionInBitMap._31) / m_ratio.width;
    positionInBitMap._32 = m_look->GetHeight() - (position.y - positionInBitMap._32) / m_ratio.height;

    positionInBitMap = positionInBitMap *
        Matrix3x2F::Rotation(360 - m_rotation, origin) *
        Matrix3x2F::Scale(m_objectScale, origin);

    position.x = positionInBitMap._31;
    position.y = positionInBitMap._32;

    return m_look->GetPixelAlphaValue(position) != 0;
}