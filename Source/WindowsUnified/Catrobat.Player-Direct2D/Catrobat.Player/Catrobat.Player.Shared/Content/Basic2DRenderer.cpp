#include "pch.h"
#include "Basic2DRenderer.h"
#include "TextureDaemon.h"
#include "ProjectDaemon.h"

#include "Common/DirectXHelper.h"

#include <windows.h>
#include <wincodec.h>

Basic2DRenderer::Basic2DRenderer(const std::shared_ptr<DX::DeviceResources>& deviceResources) :
m_deviceResources(deviceResources)
{
    ProjectDaemon::Instance()->GetProject()->StartUp();
    CreateDeviceDependentResources();
}

void Basic2DRenderer::Update(DX::StepTimer const& timer)
{
}

void Basic2DRenderer::Render()
{
    ID2D1DeviceContext1* deviceContext = m_deviceResources->GetD2DDeviceContext();

    //begin drawing operations, draw bitmap, end drawing
    deviceContext->BeginDraw();
    ProjectDaemon::Instance()->GetProject()->Render(deviceContext);
    deviceContext->EndDraw();
}

void Basic2DRenderer::CreateDeviceDependentResources()
{
    ProjectDaemon::Instance()->GetProject()->LoadTextures(m_deviceResources);
}

void Basic2DRenderer::ReleaseDeviceDependentResources()
{

}

void Basic2DRenderer::TrackingUpdate(float positionX)
{
    if (m_tracking)
    {
    }
}

void Basic2DRenderer::LoadImage()
{
    auto context1 = m_deviceResources->GetD2DDeviceContext();

    //create needed objects
    IWICImagingFactory *wicFactory = NULL;
    IWICBitmapDecoder *decoder = NULL;
    IWICBitmapFrameDecode *bitmapSrc = NULL;
    IWICFormatConverter *converter = NULL;
    ID2D1Bitmap *bitmap = NULL;

    //string path is converted to LPCWSTR format
    std::string path = ProjectDaemon::Instance()->GetProjectPath() + "\\images\\1f9cff7217d4a149801bb9e1beb344cbcb58Image";
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
    context1->CreateBitmapFromWicBitmap(converter, NULL,
        &bitmap);

    //begin drawing operations, draw bitmap, end drawing
    context1->BeginDraw();
    context1->SetTransform(D2D1::Matrix3x2F::Identity());
    context1->Clear(D2D1::ColorF(D2D1::ColorF::White));
    D2D1_SIZE_F size = bitmap->GetSize();
    D2D1_POINT_2F ulc = D2D1::Point2F(100.f, 10.f);
    context1->DrawBitmap(bitmap, D2D1::RectF(ulc.x,
        ulc.y, ulc.x + size.width, ulc.y + size.height));
    context1->EndDraw();
}

