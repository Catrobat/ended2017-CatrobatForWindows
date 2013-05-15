#include "pch.h"
#include "Sprite.h"
#include "ProjectDaemon.h"

Sprite::Sprite(string name) :
	BaseObject(),
	m_name(name),
	m_opacity(1),
	m_rotation(0.0f),
	m_lookData(NULL)
{
	m_lookDatas = new list<LookData*>();
	m_scripts = new list<Script*>();
	m_soundInfos = new list<SoundInfo*>();
}

void Sprite::addLookData(LookData *lookData)
{
	m_lookDatas->push_back(lookData);
}

void Sprite::addScript(Script *script)
{
	m_scripts->push_back(script);
}

void Sprite::addSoundInfo(SoundInfo *soundInfo)
{
	m_soundInfos->push_back(soundInfo);
}

string Sprite::getName()
{
	return m_name;
}

int Sprite::ScriptListSize()
{
	return m_scripts->size();
}

int Sprite::LookDataListSize()
{
	return m_lookDatas->size();
}

LookData *Sprite::getLookData(int index)
{
	list<LookData*>::iterator it = m_lookDatas->begin();
	advance(it, index);
	return *it;
}

Script *Sprite::getScript(int index)
{
	list<Script*>::iterator it = m_scripts->begin();
	advance(it, index);
	return *it;
}

void Sprite::LoadTextures(ID3D11Device* d3dDevice)
{
	m_position.x = 0;
	m_position.y = 0;

	for (int i = 0; i < LookDataListSize(); i++)
	{
		getLookData(i)->LoadTexture(d3dDevice);
	}
}

double radians(float degree)
{
	return degree * 3.14159265 / 180;
}

void Sprite::Draw(SpriteBatch *spriteBatch)
{
	if (m_lookData == NULL)
	{
		return;
	}

	XMFLOAT2 position;
	position.x = ProjectDaemon::Instance()->getProject()->getScreenWidth() / 2 + m_position.x;
	position.y = ProjectDaemon::Instance()->getProject()->getScreenHeight() / 2 + m_position.y;

	if (m_lookData != NULL)
		spriteBatch->Draw(m_lookData->Texture(), position, nullptr, Colors::White * m_opacity, radians(m_rotation), XMFLOAT2(m_lookData->Width() / 2, m_lookData->Height() / 2), m_objectScale, SpriteEffects_None, 0.0f);
}

void Sprite::SetLookData(int index)
{
	m_lookData = getLookData(index);
}

LookData* Sprite::GetCurrentLookData()
{
	return m_lookData;
}

Bounds Sprite::getBounds()
{
	Bounds bounds;
	bounds.x = m_position.x - m_lookData->Width() / 2;
	bounds.y = m_position.y - m_lookData->Height() / 2;
	bounds.width = (m_lookData != NULL) ? m_lookData->Width() : 0;
	bounds.height = (m_lookData != NULL) ? m_lookData->Height() : 0;
	return bounds;
}

void Sprite::StartUp()
{
	SetLookData(0);
	for (int i = 0; i < ScriptListSize(); i++)
	{
		Script *script = getScript(i);
		if (script->getType() == Script::TypeOfScript::StartScript)
			script->Execute();
	}
}

void Sprite::SetPosition(float x, float y)
{
	m_position.x = x;
	m_position.y = y;
}

void Sprite::GetPosition(float &x, float &y)
{
	x = m_position.x;
	y = m_position.y;
}

void Sprite::SetTransparency(float transparency)
{
	m_opacity = 1.0f - transparency;
}

void Sprite::SetRotation(float rotation)
{
	m_rotation = rotation;
}

float Sprite::GetRotation()
{
	return m_rotation;
}

void Sprite::SetScale(float scale)
{
	m_objectScale.x = m_objectScale.y = scale;
}

float Sprite::GetScale()
{
	return m_objectScale.x;
}