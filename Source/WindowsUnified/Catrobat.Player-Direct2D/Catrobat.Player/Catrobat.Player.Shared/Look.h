#pragma once

#include <string>
#include <D3D11.h>

#include "TextureDaemon.h"

class Look
{
public:
	Look(std::string filename, std::string name);
	~Look();

    void LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    ID2D1Bitmap* GetBitMap();
	std::string GetFileName();
	std::string GetName();
    int GetPixelAlphaValue(D2D1_POINT_2F position);

	unsigned int GetWidth();
	unsigned int GetHeight();

private:
	CatrobatTexture *m_texture;
	std::string m_filename;
	std::string m_name;
};
