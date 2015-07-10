#include "pch.h"
#include "CatrobatTexture.h"

using namespace std;

CatrobatTexture::CatrobatTexture(std::vector < std::vector<int> > alphaMap, ID2D1Bitmap *bitmap)
    : m_bitmap(bitmap), m_alphaMap(alphaMap)
{
}

shared_ptr<ID2D1Bitmap> CatrobatTexture::GetBitmap()
{
    return m_bitmap;
}

vector < vector<int> > CatrobatTexture::GetAlphaMap()
{
    return m_alphaMap;
}
