#include "pch.h"
#include "Renderer.h"

using namespace DirectX;
using namespace Microsoft::WRL;
using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Windows::Graphics::Display;

Renderer::Renderer() :
	m_loadingComplete(false),
	m_indexCount(0)
{
	m_scale = DisplayProperties::LogicalDpi / 96.0f;
}

void Renderer::CreateDeviceResources()
{
	Direct3DBase::CreateDeviceResources();

	// SpriteBatch should be create here if anyhow possible (meanwhile use Hack)
	m_spriteBatch = unique_ptr<SpriteBatch>(new SpriteBatch(m_d3dContext.Get()));
}

void Renderer::CreateWindowSizeDependentResources()
{
	Direct3DBase::CreateWindowSizeDependentResources();

	//m_testObject = new TestObject(0.0f, 0.0f, &m_windowBounds);
	//m_testObject->LoadTexture(m_d3dDevice.Get());
}

void Renderer::Update(float timeTotal, float timeDelta)
{
	// Standard Update Method (Use like in XNA :))
}

void Renderer::Render()
{
	static bool init_hack = false;
	if (!init_hack)
	{
		m_spriteBatch = unique_ptr<SpriteBatch>(new SpriteBatch(m_d3dContext.Get()));
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

	m_testObject->Draw(m_spriteBatch.get());

	m_spriteBatch->End();
	// ---------------------------------------------------------------------->
}