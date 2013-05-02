#include "pch.h"
#include "LookData.h"
#include "DDSLoader.h"

#include <vector>

using namespace DirectX;

LookData::LookData(string filename, string name) :
	m_filename(filename), m_name(name)
{
}

string LookData::Name()
{
	return m_name;
}

unsigned int LookData::Width()
{
	return m_width;
}

unsigned int LookData::Height()
{
	return m_height;
}

string LookData::FileName()
{
	return m_filename;
}

void LookData::LoadTexture(ID3D11Device* d3dDevice)
{
	m_filename = "testProject/images/" + m_filename;
	DDSLoader::LoadTexture(d3dDevice, m_filename, &m_texture, &m_width, &m_height);
}

ID3D11ShaderResourceView *LookData::Texture()
{
	return m_texture;
}