#pragma once

#include <string>
#include <D3D11.h>

#include "TextureDaemon.h"

using namespace std;

class Look
{
public:
	Look(string filename, string name);
	~Look();

    void LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources);
	ID3D11ShaderResourceView *GetResourceView();
    ID3D11Resource* GetTexture();
    ID2D1Bitmap* GetBitMap();
	string GetFileName();
	string GetName();

	unsigned int GetWidth();
	unsigned int GetHeight();

private:
	CatrobatTexture *m_texture;
	string m_filename;
	string m_name;
};
