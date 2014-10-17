#pragma once

#include "CatrobatTexture.h"
#include "Common\DeviceResources.h"

#include <map>
#include <string>
#include <d2d1.h>

class TextureDaemon
{
public:
	static TextureDaemon *Instance();

    void LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources, CatrobatTexture** texture, std::string textureKey);
private:
	TextureDaemon();

	static TextureDaemon *__instance;
	std::map<std::string, CatrobatTexture*> *m_textures;
};

