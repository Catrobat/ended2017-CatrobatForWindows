#include "pch.h"
#include "CatrobatTexture.h"


CatrobatTexture::CatrobatTexture()
{
    
}

CatrobatTexture::CatrobatTexture(ID3D11ShaderResourceView* resourceView, unsigned int width, unsigned int height, unsigned int fileSize, std::vector<unsigned char> image)
{
    this->resourceView = resourceView;
    this->height = height;
    this->width = width;
    this->fileSize = fileSize;
    this->raw_image = image;
}

CatrobatTexture::~CatrobatTexture()
{
}
