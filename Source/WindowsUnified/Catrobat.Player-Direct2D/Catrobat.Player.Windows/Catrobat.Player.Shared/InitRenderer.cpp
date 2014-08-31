#include "pch.h"
#include "InitRenderer.h"
#include "ProjectDaemon.h"
#include "DirectXHelper.h"

#include <sstream>

using namespace DirectX;
using namespace Microsoft::WRL;
using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Windows::Graphics::Display;

//--------------------------------------------------------------------------------------

InitRenderer::InitRenderer(const std::shared_ptr<DX::DeviceResources>& deviceResources) :
    m_deviceResources(deviceResources),
	m_loadingComplete(false),
	m_startup(true),
	m_indexCount(0),
    m_initialized(false)
{
	//m_scale = DisplayProperties::LogicalDpi / 96.0f;
    //CreateWindowSizeDependentResources();

    ZeroMemory(&m_textMetrics, sizeof(DWRITE_TEXT_METRICS));
    CreateDeviceIndependentResources();
    CreateDeviceDependentResources();
    CreateInitText();
}

//--------------------------------------------------------------------------------------

void InitRenderer::CreateInitText()
{
    m_text = L"Loading Project...";

    DX::ThrowIfFailed(
        m_deviceResources->GetDWriteFactory()->CreateTextLayout(
        m_text.c_str(),
        (uint32)m_text.length(),
        m_textFormat.Get(),
        240.0f, // Max width of the input text.
        50.0f, // Max height of the input text.
        &m_textLayout
        )
        );

    DX::ThrowIfFailed(
        m_textLayout->GetMetrics(&m_textMetrics)
        );
}

//--------------------------------------------------------------------------------------

void InitRenderer::CreateDeviceIndependentResources()
{
    DX::ThrowIfFailed(
        m_deviceResources->GetDWriteFactory()->CreateTextFormat(
        L"Segoe UI",
        nullptr,
        DWRITE_FONT_WEIGHT_LIGHT,
        DWRITE_FONT_STYLE_NORMAL,
        DWRITE_FONT_STRETCH_NORMAL,
        32.0f,
        L"en-US",
        &m_textFormat
        )
        );

    DX::ThrowIfFailed(
        m_textFormat->SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT_NEAR)
        );

    DX::ThrowIfFailed(
        m_deviceResources->GetD2DFactory()->CreateDrawingStateBlock(&m_stateBlock)
        );
}

//--------------------------------------------------------------------------------------

void InitRenderer::CreateDeviceDependentResources()
{
    DX::ThrowIfFailed(
        m_deviceResources->GetD2DDeviceContext()->CreateSolidColorBrush(D2D1::ColorF(D2D1::ColorF::White), &m_whiteBrush)
        );
}

//--------------------------------------------------------------------------------------

void InitRenderer::ReleaseDeviceDependentResources() {
    m_whiteBrush.Reset();
}

//--------------------------------------------------------------------------------------

//void InitRenderer::Update(float timeTotal, float timeDelta)  { }

//--------------------------------------------------------------------------------------

void InitRenderer::Render()
{
    if (!m_initialized)
	{
		m_spriteBatch = unique_ptr<SpriteBatch>(new SpriteBatch(m_deviceResources->GetD3DDeviceContext()));
		m_spriteFont = unique_ptr<SpriteFont>(new SpriteFont(m_deviceResources->GetD3DDevice(), L"italic.spritefont"));
		m_initialized = true;
	}

    auto context = m_deviceResources->GetD3DDeviceContext();

    // Reset the viewport to target the whole screen.
    auto viewport = m_deviceResources->GetScreenViewport();
    context->RSSetViewports(1, &viewport);

    // Reset render targets to the screen.
    ID3D11RenderTargetView *const targets[1] = { m_deviceResources->GetBackBufferRenderTargetView() };
    context->OMSetRenderTargets(1, targets, m_deviceResources->GetDepthStencilView());

    // Clear the back buffer and depth stencil view, clear the render target to a solid color.
    const float midnightBlue[] = { 0.098f, 0.098f, 0.439f, 1.000f };
    context->ClearRenderTargetView(
        m_deviceResources->GetBackBufferRenderTargetView(),
        midnightBlue
        );
    context->ClearDepthStencilView(m_deviceResources->GetDepthStencilView(), D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL, 1.0f, 0);

    RenderInitText();

	// SpriteBatch for Drawing. Call Draw Methods of the Objects here.
	// ---------------------------------------------------------------------->
	m_spriteBatch->Begin();
	{
		vector<string> *errors = ProjectDaemon::Instance()->GetErrorList();
		float offset = 100;
		for (unsigned int index = 0; index < errors->size(); index++)
		{
			string error = errors->at(index);
			if (error.length() > 15)
			{
				istringstream iss(error);
				vector<string> tokens;
				string buffer;
				while (iss >> buffer)
				{
					tokens.push_back(buffer);
				}

				vector<string> lines;
				string line;
				for each (string token in tokens)
				{
					line += " " + token;
					if (line.length() > 15)
					{
						lines.push_back(line);
						line = "";
					}
				}

				for each (string line in lines)
				{
					std::wstring errorString = std::wstring(line.begin(), line.end());
					const wchar_t* cerrorString = errorString.c_str();
					m_spriteFont->DrawString(m_spriteBatch.get(), cerrorString, XMFLOAT2(10, offset += 100), Colors::Black);
				}
			}
		}
	}
	m_spriteBatch->End();
	
	// ---------------------------------------------------------------------->
}

//--------------------------------------------------------------------------------------

void InitRenderer::RenderInitText()
{
    ID2D1DeviceContext* context = m_deviceResources->GetD2DDeviceContext();
    Windows::Foundation::Size logicalSize = m_deviceResources->GetLogicalSize();

    context->SaveDrawingState(m_stateBlock.Get());
    context->BeginDraw();

    D2D1::Matrix3x2F screenTranslation = D2D1::Matrix3x2F::Translation(
        logicalSize.Width - m_textMetrics.layoutWidth,
        logicalSize.Height - m_textMetrics.height * 6
        );

    context->SetTransform(screenTranslation * m_deviceResources->GetOrientationTransform2D());
    //context->SetTransform(m_deviceResources->GetOrientationTransform2D());

    DX::ThrowIfFailed(
        m_textFormat->SetTextAlignment(DWRITE_TEXT_ALIGNMENT_TRAILING)
        );

    context->DrawTextLayout(
        D2D1::Point2F(0.f, 0.f),
        m_textLayout.Get(),
        m_whiteBrush.Get()
        );

    // Ignore D2DERR_RECREATE_TARGET here. This error indicates that the device
    // is lost. It will be handled during the next call to Present.
    HRESULT hr = context->EndDraw();
    if (hr != D2DERR_RECREATE_TARGET)
    {
        DX::ThrowIfFailed(hr);
    }

    context->RestoreDrawingState(m_stateBlock.Get());
}


