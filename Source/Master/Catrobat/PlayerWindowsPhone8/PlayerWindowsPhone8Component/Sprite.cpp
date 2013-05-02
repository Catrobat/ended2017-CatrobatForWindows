#include "pch.h"
#include "Sprite.h"

Sprite::Sprite(string name) :
	BaseObject(),
	m_name(name)
{
	m_lookData = NULL;
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

void Sprite::LoadTextures(ID3D11Device* d3dDevice, Windows::Foundation::Rect *windowBounds)
{
	m_position.x = (windowBounds->Width / 2);
	m_position.y = windowBounds->Height / 2;

	for (int i = 0; i < LookDataListSize(); i++)
	{
		getLookData(i)->LoadTexture(d3dDevice);
	}
}

void Sprite::Draw(SpriteBatch *spriteBatch)
{
	if (m_lookData == NULL)
	{
		return;
	}

	if (m_lookData != NULL)
		spriteBatch->Draw(m_lookData->Texture(), m_position, nullptr, Colors::Wheat, 0.0f, XMFLOAT2(m_lookData->Width() / 2, m_lookData->Height() / 2), m_objectScale, SpriteEffects_None, 0.0f);
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
	m_position.x += x;
	m_position.y += y;
}