#pragma once
#include <vector>

class CatrobatTexture
{
public:
    CatrobatTexture();
    CatrobatTexture(ID3D11ShaderResourceView* resourceView, unsigned int width, unsigned int height, unsigned int fileSize, std::vector<unsigned char> image);
    virtual ~CatrobatTexture();

    ID3D11Resource* texture;
    ID3D11ShaderResourceView* resourceView;
    unsigned int width;
    unsigned int height;
    unsigned int fileSize;
    std::vector<unsigned char> raw_image;
};

