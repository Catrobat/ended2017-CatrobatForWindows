#include "pch.h"
#include "TextureDaemon.h"
#include "ProjectDaemon.h"
#include "DDSLoader.h"
#include "BaseException.h"
#include "PlayerException.h"

#include <sys\types.h> 
#include <sys\stat.h>
#include <exception>

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
	map<string, CatrobatTexture*>::iterator existingTexture;
	existingTexture = m_textures->find(textureKey);
    string path = ProjectDaemon::Instance()->GetProjectPath() + "/images/" + textureKey;
    int currentFileSize = GetFileSize(path);
    int oldFileSize = 0;

    if(existingTexture != m_textures->end() && existingTexture->second != NULL)
    {
        oldFileSize = existingTexture->second->fileSize;
    }

    if (existingTexture == m_textures->end() && currentFileSize != oldFileSize)
	{
		CatrobatTexture *newTexture = new CatrobatTexture();
        DDSLoader::LoadTexture(d3dDevice, path, &(newTexture->texture), &(newTexture->width), &(newTexture->height));
        newTexture->fileSize = currentFileSize;
		m_textures->insert(pair<string, CatrobatTexture*>(textureKey, newTexture));
		*texture = newTexture;
	}
	else
	{
		*texture = existingTexture->second;
	}
}

int TextureDaemon::GetFileSize(string path)
{
    struct stat filestatus;
    int error = stat( path.c_str(), &filestatus );
    
    if(error != 0)
    {
        return 0;
    }

    return filestatus.st_size;
}

