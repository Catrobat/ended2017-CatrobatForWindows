#include "pch.h"
#include "Renderer.h"
#include "ProjectDaemon.h"

using namespace DirectX;
using namespace Microsoft::WRL;
using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Windows::Graphics::Display;

Renderer::Renderer() :
	m_loadingComplete(false),
	m_startup(true),
	m_indexCount(0)
{
	m_scale = DisplayProperties::LogicalDpi / 96.0f;
}

void Renderer::CreateDeviceResources()
{
	Direct3DBase::CreateDeviceResources();
}

void Renderer::CreateWindowSizeDependentResources()
{
	Direct3DBase::CreateWindowSizeDependentResources();
}

void Renderer::Update(float timeTotal, float timeDelta)
{
}

void Renderer::Render()
{
	static bool init_hack = false;
	if (!init_hack)
	{
		m_spriteBatch = unique_ptr<SpriteBatch>(new SpriteBatch(m_d3dContext.Get()));
		m_spriteFont = unique_ptr<SpriteFont>(new SpriteFont(m_d3dDevice.Get(), L"italic.spritefont"));
		init_hack = true;
	}

	// This code is Generating a Midnightblue Background on our screen
	m_d3dContext->OMSetRenderTargets(
		1,
		m_renderTargetView.GetAddressOf(),
		m_depthStencilView.Get()
		);

	const float midnightBlue[] = { 0.098f, 0.098f, 0.439f, 1.000f };
	m_d3dContext->ClearRenderTargetView(
		m_renderTargetView.Get(),
		midnightBlue
		);

	m_d3dContext->ClearDepthStencilView(
		m_depthStencilView.Get(),
		D3D11_CLEAR_DEPTH,
		1.0f,
		0
		);

	// SpriteBatch for Drawing. Call Draw Methods of the Objects here.
	// ---------------------------------------------------------------------->
	m_spriteBatch->Begin();
	{
		/*vector<Platform::String^> *projectList = ProjectDaemon::Instance()->ProjectList();
		int offset = 0;
		for (unsigned int index = 0; index < projectList->size(); index++)
		{
			Platform::String^ rawString = projectList->at(index);
			wstring tempName(rawString->Begin());
			string filenameString(tempName.begin(), tempName.end());
			std::wstring widestr = std::wstring(filenameString.begin(), filenameString.end());
			const wchar_t* title = widestr.c_str();

			m_spriteFont->DrawString(m_spriteBatch.get(), title, XMFLOAT2(10, offset+=100), Colors::Black);

			ProjectDaemon::Instance()->OpenFolder(rawString);
		}*/

		string loadingScreen("This will be the loading screen :)");
		std::wstring loadingScreenWidestr = std::wstring(loadingScreen.begin(), loadingScreen.end());
		const wchar_t* lScreen = loadingScreenWidestr.c_str();

		m_spriteFont->DrawString(m_spriteBatch.get(), lScreen, XMFLOAT2(100, 100), Colors::Black);

		vector<string> *errors = ProjectDaemon::Instance()->ErrorList();
		int offset = 100;
		for (int index = 0; index < errors->size(); index++)
		{
			string error = errors->at(index);
			std::wstring errorString = std::wstring(error.begin(), error.end());
			const wchar_t* cerrorString = errorString.c_str();
			m_spriteFont->DrawString(m_spriteBatch.get(), cerrorString, XMFLOAT2(100, offset += 100), Colors::Black);
		}
	}
	m_spriteBatch->End();
	
	// ---------------------------------------------------------------------->
}
