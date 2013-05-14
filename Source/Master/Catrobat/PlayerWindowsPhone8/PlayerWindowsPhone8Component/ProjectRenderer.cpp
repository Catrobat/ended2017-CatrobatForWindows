#include "pch.h"
#include "ProjectRenderer.h"
#include "ProjectDaemon.h"

ProjectRenderer::ProjectRenderer()
{
}

void ProjectRenderer::CreateDeviceResources() 
{	
	Direct3DBase::CreateDeviceResources();
	UpdateForWindowSizeChange(ProjectDaemon::Instance()->getProject()->getScreenWidth(), ProjectDaemon::Instance()->getProject()->getScreenHeight());
	ProjectDaemon::Instance()->getProject()->LoadTextures(m_d3dDevice.Get(), &m_windowBounds);
}

void ProjectRenderer::CreateWindowSizeDependentResources()
{
	Direct3DBase::CreateWindowSizeDependentResources();
}

void ProjectRenderer::Render()
{
	static bool init_hack = false;
	if (!init_hack)
	{	
		ProjectDaemon::Instance()->ApplyDesiredRenderTargetSizeFromProject();
		m_spriteBatch = unique_ptr<SpriteBatch>(new SpriteBatch(m_d3dContext.Get()));
		m_spriteFont = unique_ptr<SpriteFont>(new SpriteFont(m_d3dDevice.Get(), L"italic.spritefont"));

		StartUpTasks();
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
		string loadingScreen("This is a Project :)");
		std::wstring loadingScreenWidestr = std::wstring(loadingScreen.begin(), loadingScreen.end());
		const wchar_t* lScreen = loadingScreenWidestr.c_str();

		ProjectDaemon::Instance()->getProject()->Render(m_spriteBatch.get());
	}
	m_spriteBatch->End();
	
	// ---------------------------------------------------------------------->
}

void ProjectRenderer::Update(float timeTotal, float timeDelta)
{
}

void ProjectRenderer::StartUpTasks()
{
	ProjectDaemon::Instance()->getProject()->StartUp();
}
