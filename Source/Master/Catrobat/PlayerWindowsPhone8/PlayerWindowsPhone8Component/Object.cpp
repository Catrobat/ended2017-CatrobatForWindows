#include "pch.h"
#include "Object.h"
#include "ProjectDaemon.h"

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
	m_variableList = new map<string, string>();
}

void Object::addLook(Look *lookData)
{
	m_lookList->push_back(lookData);
}

void Object::addScript(Script *script)
{
	m_scripts->push_back(script);
}

void Object::addSoundInfo(SoundInfo *soundInfo)
{
	m_soundInfos->push_back(soundInfo);
}

string Object::getName()
{
	return m_name;
}

int Object::ScriptListSize()
{
	return m_scripts->size();
}

int Object::LookDataListSize()
{
	return m_lookList->size();
}

Look *Object::getLook(int index)
{
	list<Look*>::iterator it = m_lookList->begin();
	advance(it, index);
	return *it;
}

Script *Object::getScript(int index)
{
	list<Script*>::iterator it = m_scripts->begin();
	advance(it, index);
	return *it;
}

void Object::LoadTextures(ID3D11Device* d3dDevice)
{
	m_position.x = 0;
	m_position.y = 0;

	for (int i = 0; i < LookDataListSize(); i++)
	{
		getLook(i)->LoadTexture(d3dDevice);
	}
}

double radians(float degree)
{
	return degree * 3.14159265 / 180;
}

void Object::Draw(SpriteBatch *spriteBatch)
{
	if (m_look == NULL)
	{
		return;
	}

	XMFLOAT2 position;
	position.x = ProjectDaemon::Instance()->getProject()->ScreenWidth() / 2 + m_position.x;
	position.y = ProjectDaemon::Instance()->getProject()->ScreenHeight() / 2 + m_position.y;

	if (m_look != NULL)
		spriteBatch->Draw(m_look->Texture(), position, nullptr, Colors::White * m_opacity, radians(m_rotation), XMFLOAT2(m_look->Width() / 2, m_look->Height() / 2), m_objectScale, SpriteEffects_None, 0.0f);
}

void Object::SetLook(int index)
{
	m_look = getLook(index);
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
	else
	{
		return 0;
	}
}

int Object::GetLookCount()
{
	return m_lookList->size();
}

Look* Object::GetCurrentLook()
{
	return m_look;
}

Bounds Object::getBounds()
{
	Bounds bounds;
	bounds.x = m_position.x - m_look->Width() / 2;
	bounds.y = m_position.y - m_look->Height() / 2;
	bounds.width = (m_look != NULL) ? m_look->Width() : 0;
	bounds.height = (m_look != NULL) ? m_look->Height() : 0;
	return bounds;
}

void Object::StartUp()
{
	for (int i = 0; i < ScriptListSize(); i++)
	{
		Script *script = getScript(i);
		if (script->getType() == Script::TypeOfScript::StartScript)
			script->Execute();
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
	m_objectScale.x = m_objectScale.y = scale;
}

float Object::GetScale()
{
	return m_objectScale.x;
}

void Object::addVariable(string name, string value)
{
	m_variableList->insert(pair<string, string>(name, value));
}

void Object::addVariable(pair<string, string> variable)
{
	m_variableList->insert(variable);
}

string Object::Variable(string name)
{
	map<string, string>::iterator searchItem = m_variableList->find(name);
	if (searchItem == m_variableList->end())
		return ProjectDaemon::Instance()->getProject()->Variable(name);
	if (searchItem != m_variableList->end())
		return searchItem->second;
	return "not found";
}