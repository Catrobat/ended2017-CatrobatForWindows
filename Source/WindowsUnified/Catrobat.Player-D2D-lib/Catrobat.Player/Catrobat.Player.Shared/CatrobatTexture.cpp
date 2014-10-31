#include "pch.h"
#include "CatrobatTexture.h"

using namespace std;

CatrobatTexture::CatrobatTexture()
{
    m_bitmap = nullptr;
}

CatrobatTexture::~CatrobatTexture()
{
    delete m_bitmap;
    m_bitmap = nullptr;
}

ID2D1Bitmap* CatrobatTexture::GetBitmap()
{
    return m_bitmap;
}

vector < vector<int> > CatrobatTexture::GetAlphaMap()
{
    return m_alphaMap;
}

void CatrobatTexture::SetBitmap(ID2D1Bitmap* bitmap)
{
    m_bitmap = bitmap;
}

void CatrobatTexture::SetAlphaMap(vector < vector<int> > alphaMap)
{
    m_alphaMap = alphaMap;
}
