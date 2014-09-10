#include "pch.h"
#include "TextureDaemon.h"
#include "ProjectDaemon.h"
#include "DDSLoader.h"
#include "PlayerException.h"
#include "Common/DirectXHelper.h"

#include <windows.h>
#include <wincodec.h>

using namespace std;

TextureDaemon *TextureDaemon::__instance = NULL;

TextureDaemon *TextureDaemon::Instance()
{
    if (!__instance)
    {
        __instance = new TextureDaemon();
    }

    return __instance;
}

TextureDaemon::TextureDaemon()
{
    m_textures = new map<string, CatrobatTexture*>();
}


TextureDaemon::~TextureDaemon()
{
}

void TextureDaemon::LoadTexture(ID3D11Device *d3dDevice, CatrobatTexture **texture, string textureKey)
{
    map<string, CatrobatTexture*>::iterator existingTexture;
    existingTexture = m_textures->find(textureKey);
    string path = ProjectDaemon::Instance()->GetProjectPath() + "/images/" + textureKey;
    int currentFileSize = GetFileSize(path);
    int oldFileSize = 0;

    if (existingTexture != m_textures->end() && existingTexture->second != NULL)
    {
        oldFileSize = existingTexture->second->fileSize;
    }

    if (existingTexture == m_textures->end() && currentFileSize != oldFileSize || existingTexture->second == NULL)
    {
        //TODO: Check if this needs a copy constructor for already existing textures
        CatrobatTexture *newTexture = new CatrobatTexture();
        //DDSLoader::LoadTexture(d3dDevice, path, &(newTexture->texture), &(newTexture->resourceView), &(newTexture->width), &(newTexture->height));
        newTexture->fileSize = currentFileSize;
        m_textures->insert(pair<string, CatrobatTexture*>(textureKey, newTexture));
        *texture = newTexture;
    }
    else
    {
        *texture = existingTexture->second;
    }
}

void TextureDaemon::LoadTexture(const std::shared_ptr<DX::DeviceResources>& deviceResources, CatrobatTexture** texture, std::string textureKey)
{
    auto deviceContext = deviceResources->GetD2DDeviceContext();

    //create needed objects
    IWICImagingFactory *wicFactory = NULL;
    IWICBitmapDecoder *decoder = NULL;
    IWICBitmapFrameDecode *bitmapSrc = NULL;
    IWICFormatConverter *converter = NULL;

    //string path is converted to LPCWSTR format
    std::string path = ProjectDaemon::Instance()->GetProjectPath() + "\\images\\" + textureKey;
    std::wstring stemp = std::wstring(path.begin(), path.end());
    LPCWSTR lPath = stemp.c_str();

    //create IWICFactory
    CoCreateInstance(CLSID_WICImagingFactory, NULL,
        CLSCTX_INPROC_SERVER, IID_PPV_ARGS(&wicFactory));

    //create decoder
    wicFactory->CreateDecoderFromFilename(lPath, NULL,
        GENERIC_READ, WICDecodeMetadataCacheOnDemand,
        &decoder);

    //get first frame from source
    decoder->GetFrame(0, &bitmapSrc);

    //create converter and initialize
    wicFactory->CreateFormatConverter(&converter);
    converter->Initialize(bitmapSrc,
        GUID_WICPixelFormat32bppPBGRA,
        WICBitmapDitherTypeNone, NULL, 0.f,
        WICBitmapPaletteTypeCustom);

    //create usable ID2D1Bitmap from WIC
    deviceContext->CreateBitmapFromWicBitmap(converter, NULL, &(*texture)->bitmap);

    IWICBitmapFrameDecode *pIDecoderFrame = NULL;
    decoder->GetFrame(0, &pIDecoderFrame);

    IWICBitmap *pIBitmap = NULL;
    IWICBitmapLock *pILock = NULL;

    UINT uiWidth = (*texture)->bitmap->GetSize().width;
    UINT uiHeight = (*texture)->bitmap->GetSize().height;

    WICRect rcLock = { 0, 0, uiWidth, uiHeight };

    // Create the bitmap from the image frame.
    wicFactory->CreateBitmapFromSource(
        pIDecoderFrame,          // Create a bitmap from the image frame
        WICBitmapCacheOnDemand,  // Cache metadata when needed
        &pIBitmap);              // Pointer to the bitmap

    pIBitmap->Lock(&rcLock, WICBitmapLockWrite, &pILock);

    UINT cbBufferSize = 0;
    BYTE *pv = NULL;

    // Retrieve a pointer to the pixel data.
    pILock->GetDataPointer(&cbBufferSize, &pv);


    vector<vector<int>> alphaMap;
    for (int r = 0; r < uiHeight; r++)
    {
        vector<int> row;
        for (int c = 0; c < uiWidth; c++)
        {
            row.push_back(pv[(r * uiWidth + c) * 4 + 3]);
        }
        alphaMap.push_back(row);
    }
    (*texture)->alphaMap = alphaMap;
}

int TextureDaemon::GetFileSize(string path)
{
    //struct stat filestatus;
    //int error = stat( path.c_str(), &filestatus );
    //
    //if(error != 0)
    //{
    //    return 0;
    //}

    //return filestatus.st_size;
    return 1;
}

