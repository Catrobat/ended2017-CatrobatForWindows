#pragma once
#include <vector>

class CatrobatTexture
{
public:
    CatrobatTexture();
    ~CatrobatTexture();

public:
    ID2D1Bitmap* GetBitmap();
    std::vector < std::vector<int> > GetAlphaMap();

    void SetBitmap(ID2D1Bitmap* bitmap);
    void SetAlphaMap(std::vector < std::vector<int> > alphaMap);

private:
    ID2D1Bitmap* m_bitmap;
    std::vector < std::vector<int> > m_alphaMap;
};

