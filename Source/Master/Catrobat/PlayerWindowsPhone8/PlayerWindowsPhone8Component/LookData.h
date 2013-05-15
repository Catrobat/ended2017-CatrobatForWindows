#pragma once

#include <string>
#include <D3D11.h>

#include "DDSTextureLoader.h"
#include "TextureDaemon.h"

using namespace std;

class LookData
{
public:
	LookData(string filename, string name);
	~LookData();

	void LoadTexture(ID3D11Device* d3dDevice);
	ID3D11ShaderResourceView *Texture();

	string FileName();
	string Name();

	unsigned int Width();
	unsigned int Height();

private:
	CatrobatTexture *m_texture;
	string m_filename;
	string m_name;
};
