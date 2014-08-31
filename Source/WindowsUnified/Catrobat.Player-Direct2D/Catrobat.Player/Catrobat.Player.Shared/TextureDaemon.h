#pragma once

#include <map>
#include <string>
#include <D3D11.h>
#include "CatrobatTexture.h"

//struct CatrobatTexture
//{
//	ID3D11ShaderResourceView* texture;
//	unsigned int width;
//	unsigned int height;
//    unsigned int fileSize;
//    vector<unsigned char> raw_image;
//};

class TextureDaemon
{
public:
	static TextureDaemon *Instance();

	void LoadTexture(ID3D11Device *d3dDevice, CatrobatTexture **texture, std::string textureKey);
private:
	TextureDaemon();
	~TextureDaemon();

	static TextureDaemon *__instance;
	int GetFileSize(std::string path);
	std::map<std::string, CatrobatTexture*> *m_textures;
};

