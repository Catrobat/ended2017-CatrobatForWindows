#pragma once
#include <vector>

class CatrobatTexture
{
public:
    CatrobatTexture();
    virtual ~CatrobatTexture();

    ID3D11Resource* texture;
    ID2D1Bitmap* bitmap;
    std::vector < std::vector<int> > alphaMap;
    ID3D11ShaderResourceView* resourceView;
    unsigned int fileSize;
    std::vector<unsigned char> raw_image;
};

