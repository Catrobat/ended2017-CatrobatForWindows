#include "pch.h"
#include "ProjectRenderer.h"
#include "ProjectDaemon.h"
#include "PlayerException.h"

#include <exception>
ProjectRenderer::ProjectRenderer()
{
    m_Initialized = false;
}

ProjectRenderer::~ProjectRenderer()
{
    m_Initialized = false;
}

void ProjectRenderer::CreateDeviceResources() 
{	
	Direct3DBase::CreateDeviceResources();
	UpdateForWindowSizeChange((float) ProjectDaemon::Instance()->GetProject()->GetScreenWidth(), (float) ProjectDaemon::Instance()->GetProject()->GetScreenHeight());
	ProjectDaemon::Instance()->GetProject()->LoadTextures(m_d3dDevice.Get());
}

void ProjectRenderer::CreateWindowSizeDependentResources()
{
	Direct3DBase::CreateWindowSizeDependentResources();
}

void ProjectRenderer::Render()
{

	if (!m_Initialized)
	{	
		//ProjectDaemon::Instance()->ApplyDesiredRenderTargetSizeFromProject();
		m_spriteBatch = unique_ptr<SpriteBatch>(new SpriteBatch(m_d3dContext.Get()));
		m_spriteFont = unique_ptr<SpriteFont>(new SpriteFont(m_d3dDevice.Get(), L"italic.spritefont"));

		StartUpTasks();
		m_Initialized = true;
	}

	// This code is Generating a Midnightblue Background on our screen
	m_d3dContext->OMSetRenderTargets(
		1,
		m_renderTargetView.GetAddressOf(),
		m_depthStencilView.Get()
		);

	const float whiteBackground[] = { 1.000f, 1.000f, 1.000f, 1.000f };
	m_d3dContext->ClearRenderTargetView(
		m_renderTargetView.Get(),
		whiteBackground
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
		ProjectDaemon::Instance()->GetProject()->Render(m_spriteBatch.get());
	}
	m_spriteBatch->End();
	
	// ---------------------------------------------------------------------->
}

void ProjectRenderer::Update(float timeTotal, float timeDelta)
{
}

void ProjectRenderer::StartUpTasks()
{
	ProjectDaemon::Instance()->GetProject()->StartUp();
}
