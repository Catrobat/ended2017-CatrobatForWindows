#include "pch.h"
#include "Look.h"
#include "DDSLoader.h"
#include "ProjectDaemon.h"
#include "PlayerException.h"

#include <vector>

using namespace DirectX;
using namespace std;

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
    if (m_texture == NULL)
    {
        throw new PlayerException("Look::GetWidth called with no texture defined.");
    }
    return m_texture->bitmap->GetSize().width;
}

unsigned int Look::GetHeight()
{
    if (m_texture == NULL)
    {
        throw new PlayerException("Look::GetHeight called with no texture defined.");
    }
    return m_texture->bitmap->GetSize().height;
}

string Look::GetFileName()
{
    return m_filename;
}

void Look::LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources)
{
    TextureDaemon::Instance()->LoadTexture(deviceResources, &m_texture, m_filename);
}

ID3D11ShaderResourceView *Look::GetResourceView()
{
    if (m_texture == NULL)
    {
        throw new PlayerException("Look::GetResourceView called with no texture defined.");
    }
    return m_texture->resourceView;
}

ID3D11Resource *Look::GetTexture()
{
    if (m_texture == NULL)
    {
        throw new PlayerException("Look::GetTexture called with no texture defined.");
    }
    return m_texture->texture;
}

ID2D1Bitmap *Look::GetBitMap()
{
    if (m_texture == NULL)
    {
        throw new PlayerException("Look::GetTexture called with no texture defined.");
    }
    return m_texture->bitmap;
}

int Look::GetPixelAlphaValue(D2D1_POINT_2F position)
{
    if (m_texture == NULL)
    {
        throw new PlayerException("Look::No texture defined.");
    }
    return m_texture->alphaMap.at(position.y).at(position.x);
}