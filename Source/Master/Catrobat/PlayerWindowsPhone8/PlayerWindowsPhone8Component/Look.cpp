#include "pch.h"
#include "Look.h"
#include "DDSLoader.h"
#include "ProjectDaemon.h"
#include <vector>

using namespace DirectX;

Look::Look(string filename, string name) :
	m_filename(filename), m_name(name)
{
	m_texture = new CatrobatTexture();
}

string Look::GetName()
{
	return m_name;
}

unsigned int Look::GetWidth()
{
	return m_texture->width;
}

unsigned int Look::GetHeight()
{
	return m_texture->height;
}

string Look::GetFileName()
{
	return m_filename;
}

void Look::LoadTexture(ID3D11Device* d3dDevice)
{
	TextureDaemon::Instance()->LoadTexture(d3dDevice, &m_texture, m_filename);
}

ID3D11ShaderResourceView *Look::GetTexture()
{
	return m_texture->texture;
}