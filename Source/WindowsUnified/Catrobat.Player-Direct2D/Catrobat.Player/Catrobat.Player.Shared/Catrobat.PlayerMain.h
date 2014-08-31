#pragma once

#include "Common\StepTimer.h"
#include "Common\DeviceResources.h"
#include "Content\Sample3DSceneRenderer.h"
#include "Content\SampleFpsTextRenderer.h"
#include "Content\Basic2DRenderer.h"

// Renders Direct2D and 3D content on the screen.
namespace Catrobat_Player
{
	class Catrobat_PlayerMain : public DX::IDeviceNotify
	{
	public:
		Catrobat_PlayerMain(const std::shared_ptr<DX::DeviceResources>& deviceResources);
		~Catrobat_PlayerMain();
		void CreateWindowSizeDependentResources();
		void StartTracking() { m_basic2dRenderer->StartTracking(); }
		void TrackingUpdate(float positionX) { m_pointerLocationX = positionX; }
        void StopTracking() { m_basic2dRenderer->StopTracking(); }
        bool IsTracking() { return m_basic2dRenderer->IsTracking(); }
		void StartRenderLoop();
		void StopRenderLoop();
		Concurrency::critical_section& GetCriticalSection() { return m_criticalSection; }

		// IDeviceNotify
		virtual void OnDeviceLost();
		virtual void OnDeviceRestored();

	private:
		void ProcessInput();
		void Update();
		bool Render();

		// Cached pointer to device resources.
		std::shared_ptr<DX::DeviceResources> m_deviceResources;

		// TODO: Replace with your own content renderers.
		std::unique_ptr<Sample3DSceneRenderer> m_sceneRenderer;
		std::unique_ptr<SampleFpsTextRenderer> m_fpsTextRenderer;
        std::unique_ptr<Basic2DRenderer> m_basic2dRenderer;

		Windows::Foundation::IAsyncAction^ m_renderLoopWorker;
		Concurrency::critical_section m_criticalSection;

		// Rendering loop timer.
		DX::StepTimer m_timer;

		// Track current input pointer position.
		float m_pointerLocationX;

        bool m_loadingComplete;
	};
}