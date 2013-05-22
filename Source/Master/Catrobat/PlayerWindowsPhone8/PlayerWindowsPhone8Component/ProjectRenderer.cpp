#include "pch.h"
#include "ProjectRenderer.h"
#include "ProjectDaemon.h"

ProjectRenderer::ProjectRenderer()
{
	m_accelerometer = Windows::Devices::Sensors::Accelerometer::GetDefault();

	// Only use this if you have it
	//m_gyrometer = Windows::Devices::Sensors::Gyrometer::GetDefault();
}

void ProjectRenderer::CreateDeviceResources() 
{	
	Direct3DBase::CreateDeviceResources();
	UpdateForWindowSizeChange(ProjectDaemon::Instance()->getProject()->ScreenWidth(), ProjectDaemon::Instance()->getProject()->ScreenHeight());
	ProjectDaemon::Instance()->getProject()->LoadTextures(m_d3dDevice.Get());
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
		ProjectDaemon::Instance()->getProject()->Render(m_spriteBatch.get());
	}
	m_spriteBatch->End();
	
	// ---------------------------------------------------------------------->
}

void ProjectRenderer::Update(float timeTotal, float timeDelta)
{
	// Reading Accelerometer Data
	if (m_accelerometer != nullptr)
    {
		try
		{
			m_accReading = m_accelerometer->GetCurrentReading();
			Platform::String ^acceleration = L"Acceleration: " + "X: " + m_accReading->AccelerationX + " Y: " + m_accReading->AccelerationY + " Z: " + m_accReading->AccelerationZ;	
		}
		catch(Platform::Exception^ e)
		{
			// there is a bug tracking this issue already
			// we need to remove this try\catch once the bug # 158858 hits our branch
			// For now, to make this App work, catching the exception
			// The reverting is tracked by WP8 # 159660
		}
	}

	// Reading Gyrometer Data
	/*
	if (m_gyrometer != nullptr)
    {
        try
        {
            m_gyroReading = m_gyrometer->GetCurrentReading();
            Platform::String ^gyroAngularVel = L"Gyro angular velocity X: " + m_gyroReading->AngularVelocityX + " Y: " + m_gyroReading->AngularVelocityY + " Z: " + m_gyroReading->AngularVelocityZ;		
        }
		catch(Platform::Exception^ e)
		{
			// there is a bug tracking this issue already
			// we need to remove this try\catch once the bug # 158858 hits our branch
			// For now, to make this App work, catching the exception
            // The reverting is tracked by WP8 # 159660
        }
    }
	*/
}

void ProjectRenderer::StartUpTasks()
{
	ProjectDaemon::Instance()->getProject()->StartUp();
}
