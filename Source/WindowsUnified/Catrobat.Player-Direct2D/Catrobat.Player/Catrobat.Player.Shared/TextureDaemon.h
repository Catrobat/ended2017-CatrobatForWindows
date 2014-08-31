#pragma once

#include "CatrobatTexture.h"

#include <map>
#include <string>
#include <d2d1.h>

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
    void LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources, CatrobatTexture** texture, std::string textureKey);
private:
	TextureDaemon();
	~TextureDaemon();

	static TextureDaemon *__instance;
	int GetFileSize(std::string path);
	std::map<std::string, CatrobatTexture*> *m_textures;
};

