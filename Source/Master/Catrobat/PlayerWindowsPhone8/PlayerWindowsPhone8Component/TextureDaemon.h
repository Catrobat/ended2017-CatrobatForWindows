#pragma once

#include <map>
#include <string>
#include <D3D11.h>

struct CatrobatTexture
{
	ID3D11ShaderResourceView* texture;
	unsigned int width;
	unsigned int height;
};

class TextureDaemon
{
public:
	static TextureDaemon *Instance();

	void LoadTexture(ID3D11Device *d3dDevice, CatrobatTexture **texture, std::string textureKey);

private:
	TextureDaemon();
	~TextureDaemon();

	static TextureDaemon *__instance;
	
	std::map<std::string, CatrobatTexture*> *m_textures;
};

