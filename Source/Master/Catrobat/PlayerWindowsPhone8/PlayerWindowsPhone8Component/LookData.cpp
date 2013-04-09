#include "pch.h"
#include "LookData.h"

using namespace DirectX;

LookData::LookData(string filename, string name) :
	m_filename(filename), m_name(name)
{
}

string LookData::Name()
{
	return m_name;
}

string LookData::FileName()
{
	return m_filename;
}

void LookData::LoadTexture(ID3D11Device* d3dDevice)
{
	CreateDDSTextureFromFile(d3dDevice, L"testProject/images/34A109A82231694B6FE09C216B390570_normalCat.dds", nullptr, &m_Texture, MAXSIZE_T);
}

ID3D11ShaderResourceView *LookData::Texture()
{
	return m_Texture;
}