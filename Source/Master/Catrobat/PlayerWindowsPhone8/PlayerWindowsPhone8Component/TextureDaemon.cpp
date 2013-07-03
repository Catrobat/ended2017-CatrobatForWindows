#include "pch.h"
#include "TextureDaemon.h"
#include "ProjectDaemon.h"
#include "DDSLoader.h"

using namespace std;

TextureDaemon *TextureDaemon::__instance = NULL;

TextureDaemon *TextureDaemon::Instance()
{
	if (!__instance)
		__instance = new TextureDaemon();
	return __instance;
}

TextureDaemon::TextureDaemon()
{
	m_textures = new map<string, CatrobatTexture*>();
}


TextureDaemon::~TextureDaemon()
{
}

void TextureDaemon::LoadTexture(ID3D11Device *d3dDevice, CatrobatTexture **texture, string textureKey)
{
	map<string, CatrobatTexture*>::iterator currentTexture;
	currentTexture = m_textures->find(textureKey);
	
	if (currentTexture == m_textures->end())
	{
		CatrobatTexture *newTexture = new CatrobatTexture();
		string path = ProjectDaemon::Instance()->GetProjectPath() + "/images/" + textureKey;
		DDSLoader::LoadTexture(d3dDevice, path, &(newTexture->texture), &(newTexture->width), &(newTexture->height));

		m_textures->insert(pair<string, CatrobatTexture*>(textureKey, newTexture));
		*texture = newTexture;
	}
	else
	{
		*texture = currentTexture->second;
	}
}
