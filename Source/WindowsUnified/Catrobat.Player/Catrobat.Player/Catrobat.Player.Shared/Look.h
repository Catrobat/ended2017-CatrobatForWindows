#pragma once

#include <string>
#include <D3D11.h>

#include "TextureDaemon.h"

class Look
{
public:
    Look(std::string filename, std::string name);

    void LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    std::shared_ptr<ID2D1Bitmap> GetBitMap();
    std::string GetFileName();
    std::string GetName();
    int GetPixelAlphaValue(D2D1_POINT_2F position);
	
    unsigned int GetWidth();
    unsigned int GetHeight();

private:
    std::unique_ptr<CatrobatTexture> m_texture;
    std::string m_filename;
    std::string m_name;
	std::vector<std::vector<int>> m_alphamap;

#if PSAPI_VERSION
// Code for Catrobat.Player.WindowsPhone.Tests
public: 
	void SetAlphaMap(std::vector<std::vector<int>> aMap);
	std::vector<std::vector<int>> GetAlphaMap() { return m_alphamap; };
#endif

};
