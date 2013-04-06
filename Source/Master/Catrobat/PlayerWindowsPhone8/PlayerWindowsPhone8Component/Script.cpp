#include "pch.h"
#include "Script.h"

Script::Script(TypeOfScript scriptType, string spriteReference) :
	BaseObject(50, 50, 1, 1, 0, 0),
	m_scriptType(scriptType), m_spriteReference(spriteReference)
{
	m_brickList = new list<Brick*>();
}

void Script::addBrick(Brick *brick)
{
	m_brickList->push_back(brick);
}

void Script::addSpriteReference(string spriteReference)
{
	m_spriteReference = spriteReference;
}

Script::TypeOfScript Script::getType()
{
	return m_scriptType;
}

string Script::SpriteReference()
{
	return m_spriteReference;
}

int Script::BrickListSize()
{
	return m_brickList->size();
}

Brick *Script::GetBrick(int index)
{
	list<Brick*>::iterator it = m_brickList->begin();
	advance(it, index);
	return *it;
}

void Script::Render(SpriteBatch *spriteBatch)
{
	for (int i = 0; i < BrickListSize(); i++)
	{
		GetBrick(i)->Render(spriteBatch);
	}
	Draw(spriteBatch);
}

void Script::LoadTextures(ID3D11Device* d3dDevice)
{
	for (int i = 0; i < BrickListSize(); i++)
	{
		GetBrick(i)->LoadTextures(d3dDevice);
	}
	LoadTexture(d3dDevice);
}

void Script::Draw(SpriteBatch *spriteBatch)
{
	spriteBatch->Draw(m_Texture, m_position, nullptr, Colors::Wheat, 0.0f, m_sourceOrigin, m_objectScale, SpriteEffects_None, 0.0f);
}

void Script::LoadTexture(ID3D11Device* d3dDevice)
{
	CreateDDSTextureFromFile(d3dDevice, L"script.dds", nullptr, &m_Texture, MAXSIZE_T);
}