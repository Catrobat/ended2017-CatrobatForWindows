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

InitRenderer::InitRenderer(const std::shared_ptr<DX::Direct3DDeviceResources>& direct3DDeviceResources) :
    m_direct3DDeviceResources(direct3DDeviceResources),
	m_loadingComplete(false),
	m_startup(true),
	m_indexCount(0),
    m_initialized(false)
{
	//m_scale = DisplayProperties::LogicalDpi / 96.0f;

    //CreateDeviceDependentResources();
    //CreateWindowSizeDependentResources();
}

//void InitRenderer::Update(float timeTotal, float timeDelta)  { }

void InitRenderer::Render()
{
    if (!m_initialized)
	{
		//m_spriteBatch = unique_ptr<SpriteBatch>(new SpriteBatch(m_direct3DDeviceResources->GetD3DDevice()));
		//m_spriteFont = unique_ptr<SpriteFont>(new SpriteFont(m_d3dDevice.Get(), L"italic.spritefont"));
		m_initialized = true;
	}

    // Process events incoming to the window.
    m_window->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);

	// This code is Generating a Midnightblue Background on our screen
    m_direct3DDeviceResources->m_d3dContext->OMSetRenderTargets(
        1,
        m_direct3DDeviceResources->m_d3dRenderTargetView.GetAddressOf(),
        nullptr //m_direct3DDeviceResources->m_d3dDepthStencilView.Get()
        );

    const float midnightBlue[] = { 0.098f, 0.098f, 0.439f, 1.000f };
    m_direct3DDeviceResources->m_d3dContext->ClearRenderTargetView(
        m_direct3DDeviceResources->m_d3dRenderTargetView.Get(),
        midnightBlue
        );

    //m_direct3DDeviceResources->m_d3dContext->ClearDepthStencilView(
    //    m_direct3DDeviceResources->m_d3dDepthStencilView.Get(),
    //    D3D11_CLEAR_DEPTH,
    //    1.0f,
    //    0
    //    );

    DX::ThrowIfFailed(
        m_direct3DDeviceResources->m_swapChain->Present(1, 0)
        );
    
 //   m_d3dContext->OMSetRenderTargets(
	//	1,
	//	m_renderTargetView.GetAddressOf(),
	//	m_depthStencilView.Get()
	//	);

	//const float midnightBlue[] = { 0.098f, 0.098f, 0.439f, 1.000f };
	//m_d3dContext->ClearRenderTargetView(
	//	m_renderTargetView.Get(),
	//	midnightBlue
	//	);

	//m_d3dContext->ClearDepthStencilView(
	//	m_depthStencilView.Get(),
	//	D3D11_CLEAR_DEPTH,
	//	1.0f,
	//	0
	//	);

	// SpriteBatch for Drawing. Call Draw Methods of the Objects here.
	// ---------------------------------------------------------------------->
	//m_spriteBatch->Begin();
	//{
	//	vector<string> *errors = ProjectDaemon::Instance()->GetErrorList();
	//	float offset = 100;
	//	for (unsigned int index = 0; index < errors->size(); index++)
	//	{
	//		string error = errors->at(index);
	//		if (error.length() > 15)
	//		{
	//			istringstream iss(error);
	//			vector<string> tokens;
	//			string buffer;
	//			while (iss >> buffer)
	//			{
	//				tokens.push_back(buffer);
	//			}

	//			vector<string> lines;
	//			string line;
	//			for each (string token in tokens)
	//			{
	//				line += " " + token;
	//				if (line.length() > 15)
	//				{
	//					lines.push_back(line);
	//					line = "";
	//				}
	//			}

	//			for each (string line in lines)
	//			{
	//				std::wstring errorString = std::wstring(line.begin(), line.end());
	//				const wchar_t* cerrorString = errorString.c_str();
	//				m_spriteFont->DrawString(m_spriteBatch.get(), cerrorString, XMFLOAT2(10, offset += 100), Colors::Black);
	//			}
	//		}
	//	}
	//}
	//m_spriteBatch->End();
	
	// ---------------------------------------------------------------------->
}
