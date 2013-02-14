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
}

void Renderer::CreateWindowSizeDependentResources()
{
	Direct3DBase::CreateWindowSizeDependentResources();

	//CreateTestObject1();
	//CreateTestObject2();
	CreateTestObject3();
	//CreateTestObject4();
	m_testObject->LoadTexture(m_d3dDevice.Get());
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

void Renderer::CreateTestObject1()
{
	// TestObject(float posX, float posY, Rect *windowBounds, float originX, float originY);
	m_testObject = new TestObject(50, 50, &m_windowBounds);
}

void Renderer::CreateTestObject2()
{
	// TestObject(float x, float y, float width, float height, float originX, float originY);
	m_testObject = new TestObject(50, 50, 100, 100, 0, 0);
}

void Renderer::CreateTestObject3()
{
	// TestObject(Rect *position, float originX, float originY);
	Rect position(50, 50, 100, 100);
	m_testObject = new TestObject(position);
}

void Renderer::CreateTestObject4()
{
	// TestObject(Point location, Size size, float originX = 0, float originY = 0); 
	Point location(50, 50);
	Size size(100, 100);
	m_testObject = new TestObject(location, size);
}