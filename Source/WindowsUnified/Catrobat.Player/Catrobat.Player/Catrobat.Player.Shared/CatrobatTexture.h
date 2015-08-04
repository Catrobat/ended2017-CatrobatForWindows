#pragma once
#include <vector>

class CatrobatTexture
{
public:
    CatrobatTexture(std::vector < std::vector<int> > alphaMap, Microsoft::WRL::ComPtr<ID2D1Bitmap> bitmap);
	~CatrobatTexture();

public:
	Microsoft::WRL::ComPtr<ID2D1Bitmap> GetBitmap();
    std::vector < std::vector<int> > GetAlphaMap();

private:
	Microsoft::WRL::ComPtr<ID2D1Bitmap> m_bitmap;
    std::vector < std::vector<int> > m_alphaMap;
};

