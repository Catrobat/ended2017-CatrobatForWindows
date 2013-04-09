#pragma once

#include <string>
#include <D3D11.h>

#include "DDSTextureLoader.h"

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

private:
	ID3D11ShaderResourceView* m_Texture;
	string m_filename;
	string m_name;
};
