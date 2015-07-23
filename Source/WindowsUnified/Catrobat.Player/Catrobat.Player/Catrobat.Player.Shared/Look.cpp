#include "pch.h"
#include "Look.h"
#include "DDSLoader.h"
#include "ProjectDaemon.h"
#include "PlayerException.h"
#include "OutOfBoundsException.h"

#include <math.h>
#include <vector>

using namespace DirectX;
using namespace std;

Look::Look(string filename, string name) :
m_filename(filename), m_name(name)
{
}

string Look::GetName()
{
    return m_name;
}

unsigned int Look::GetWidth()
{
    if (m_texture == NULL)
    {
        throw PlayerException("Look::GetWidth called with no texture defined.");
    }
    return (unsigned int) m_texture->GetBitmap()->GetSize().width;
}

unsigned int Look::GetHeight()
{
    if (m_texture == NULL)
    {
        throw PlayerException("Look::GetHeight called with no texture defined.");
    }
    return (unsigned int) m_texture->GetBitmap()->GetSize().height;
}

string Look::GetFileName()
{
    return m_filename;
}

void Look::LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources)
{
    TextureDaemon::Instance()->LoadTexture(deviceResources, move(m_texture), m_filename);
	m_alphamap = m_texture->GetAlphaMap();
}

shared_ptr<ID2D1Bitmap> Look::GetBitMap()
{
    if (m_texture == NULL)
    {
        throw PlayerException("Look::GetTexture called with no texture defined.");
    }
    return m_texture->GetBitmap();
}

int Look::GetPixelAlphaValue(D2D1_POINT_2F position)
{
	if (m_alphamap.empty())
		return 0;

    int x = (int) round(position.x);
    int y = (int) round(position.y);

    if (x < 0 || y < 0)
    {
		return 0;
    }
    else if (m_alphamap.size() <= y || m_alphamap.at(y).size() <= x)
    {
		return 0;
    }
    return m_alphamap.at(y).at(x);
}

#if PSAPI_VERSION
// Code for Catrobat.Player.WindowsPhone.Tests
void Look::SetAlphaMap(std::vector<std::vector<int>> aMap)
{
	m_alphamap = aMap;
}
#endif