#include "pch.h"
#include "Object.h"
#include "ProjectDaemon.h"
#include "PlayerException.h"

#include <exception>

Object::Object(string name) :
BaseObject(),
m_name(name),
m_opacity(1),
m_rotation(0.0f),
m_look(NULL)
{
    m_lookList = new list<Look*>();
    m_scripts = new list<Script*>();
    m_soundInfos = new list<SoundInfo*>();
    m_variableList = new map<string, UserVariable*>();
}

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

string Object::GetName()
{
    return m_name;
}

int Object::GetScriptListSize()
{
    return m_scripts->size();
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

Script *Object::GetScript(int index)
{
    list<Script*>::iterator it = m_scripts->begin();
    advance(it, index);
    return *it;
}

void Object::LoadTextures(ID3D11Device* d3dDevice)
{
    m_position.x = 0;
    m_position.y = 0;

    for (int i = 0; i < GetLookDataListSize(); i++) //TODO: implement dummy texture if there is no texture found
    {
        auto look = GetLook(i);

        if (look != NULL)
        {
            GetLook(i)->LoadTexture(d3dDevice);
        }
    }
}

double radians(float degree)
{
    return degree * 3.14159265 / 180.0f;
}

/*
Draw the current look of this object.
*/
void Object::Draw(SpriteBatch *spriteBatch)
{
    if (m_look == NULL)
    {
        return;
    }

    XMFLOAT2 position;
    position.x = ProjectDaemon::Instance()->GetProject()->GetScreenWidth() / 2.0f + m_position.x;
    position.y = ProjectDaemon::Instance()->GetProject()->GetScreenHeight() / 2.0f + m_position.y;

    if (m_look != NULL)
    {
        spriteBatch->Draw(m_look->GetTexture(), position, nullptr,
            Colors::White * m_opacity, (float)radians(m_rotation), XMFLOAT2(((float)m_look->GetWidth()) / 2.0f,
            ((float)m_look->GetHeight()) / 2.0f), m_objectScale, SpriteEffects_None, 0.0f);
    }
}

void Object::SetLook(int index)
{
    m_look = GetLook(index);
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

Bounds Object::GetBounds()
{
    Bounds bounds;
    bounds.x = 0.0f;
    bounds.y = 0.0f;
    bounds.width = 0.0f;
    bounds.height = 0.0f;

    auto currentLook = GetCurrentLook();

    if (currentLook != NULL)
    {
        bounds.x = m_position.x - currentLook->GetWidth() / 2.0f;
        bounds.y = m_position.y - currentLook->GetHeight() / 2.0f;
        bounds.width = (float)currentLook->GetWidth();
        bounds.height = (float)currentLook->GetHeight();
    }

    return bounds;
}

//Executes all Startscripts of the object and sets the first look if the
//costume list is not empty.
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

void Object::SetPosition(float x, float y)
{
    m_position.x = x;
    m_position.y = y;
}

void Object::GetPosition(float &x, float &y)
{
    x = m_position.x;
    y = m_position.y;
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
}

float Object::GetRotation()
{
    return m_rotation;
}

void Object::SetScale(float scale)
{
    if (scale < 0.0f)
    {
        scale = 0.0f;
    }

    m_objectScale.x = m_objectScale.y = scale / 100.0f;
}

float Object::GetScale()
{
    return m_objectScale.x * 100.0f;
}

void Object::AddVariable(string name, UserVariable* variable)
{
    m_variableList->insert(pair<string, UserVariable*>(name, variable));
}

void Object::AddVariable(pair<string, UserVariable*> variable)
{
    m_variableList->insert(variable);
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

bool Object::IsTapPointHitting(int xPosition, int yPosition, int xNormalized, int yNormalized)
{
    auto bounds = GetBounds();

    if (bounds.x <= xNormalized &&
        bounds.y <= yNormalized &&
        (bounds.x + bounds.width) >= xNormalized &&
        (bounds.y + bounds.height) >= yNormalized)
    {
        return true;
    }

    return false;
}