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
	m_filename = "testProject/images/" + m_filename + ".dds";
	std::wstring widestr = std::wstring(m_filename.begin(), m_filename.end());
	const wchar_t* widecstr = widestr.c_str();
	CreateDDSTextureFromFile(d3dDevice, widecstr, nullptr, &m_Texture, MAXSIZE_T);
}

ID3D11ShaderResourceView *LookData::Texture()
{
	return m_Texture;
}