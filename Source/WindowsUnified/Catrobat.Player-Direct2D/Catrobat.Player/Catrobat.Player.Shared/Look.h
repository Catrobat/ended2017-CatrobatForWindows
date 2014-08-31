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

	void LoadTexture(ID3D11Device* d3dDevice);
	ID3D11ShaderResourceView *GetResourceView();
    ID3D11Resource* GetTexture();
	string GetFileName();
	string GetName();

	unsigned int GetWidth();
	unsigned int GetHeight();

private:
	CatrobatTexture *m_texture;
	string m_filename;
	string m_name;
};
