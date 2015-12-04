#include "pch.h"
#include "CatrobatTexture.h"

using namespace std;
using namespace Microsoft::WRL;

CatrobatTexture::CatrobatTexture(vector < vector<int> > alphaMap, ComPtr<ID2D1Bitmap> bitmap)
	: m_bitmap(move(bitmap)), m_alphaMap(alphaMap)
{
}

CatrobatTexture::~CatrobatTexture()
{
	m_bitmap.Reset();
}

ComPtr<ID2D1Bitmap> CatrobatTexture::GetBitmap()
{
	return m_bitmap;
}

vector < vector<int> > CatrobatTexture::GetAlphaMap()
{
	return m_alphaMap;
}
