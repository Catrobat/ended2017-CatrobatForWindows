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

TextureDaemon::TextureDaemon() {}

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
    ID2D1Bitmap* bitmap;
    deviceContext->CreateBitmapFromWicBitmap(converter, NULL, &bitmap);

    IWICBitmapFrameDecode *pIDecoderFrame = NULL;
    decoder->GetFrame(0, &pIDecoderFrame);

    IWICBitmap *pIBitmap = NULL;
    IWICBitmapLock *pILock = NULL;

    UINT uiWidth = bitmap->GetSize().width;
    UINT uiHeight = bitmap->GetSize().height;

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
    (*texture)->SetAlphaMap(alphaMap);
    (*texture)->SetBitmap(bitmap);
}

