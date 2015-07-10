#pragma once
#include <vector>

class CatrobatTexture
{
public:
    CatrobatTexture(std::vector < std::vector<int> > alphaMap, ID2D1Bitmap *bitmap);

public:
    std::shared_ptr<ID2D1Bitmap> GetBitmap();
    std::vector < std::vector<int> > GetAlphaMap();

private:
    std::shared_ptr<ID2D1Bitmap> m_bitmap;
    std::vector < std::vector<int> > m_alphaMap;
};

