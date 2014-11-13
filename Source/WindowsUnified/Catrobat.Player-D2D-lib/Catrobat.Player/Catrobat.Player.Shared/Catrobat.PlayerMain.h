#pragma once

#include "Common\StepTimer.h"
#include "Common\DeviceResources.h"
//#include "Content\Sample3DSceneRenderer.h"
#include "Content\SampleFpsTextRenderer.h"
#include "Content\Basic2DRenderer.h"

// Renders Direct2D and 3D content on the screen.
namespace Catrobat_Player
{
    enum class PlayerState
    {
        Active,
        Pause,
        Init,
    };

	class Catrobat_PlayerMain : public DX::IDeviceNotify
	{
	public:
        Catrobat_PlayerMain(const std::shared_ptr<DX::DeviceResources>& deviceResources, 
                            Windows::UI::Xaml::Controls::CommandBar^ playerAppBar);
		~Catrobat_PlayerMain();
		void CreateWindowSizeDependentResources();
        Concurrency::critical_section& GetCriticalSection() { return m_criticalSection; }
        
        // Render functionality.
		void StartRenderLoop();
		void StopRenderLoop();
		
        // Event input from the user.
        void PointerPressed(D2D1_POINT_2F point);
        bool HardwareBackButtonPressed();
        void RestartButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void PlayButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void ThumbnailButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void EnableAxisButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void ScreenshotButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);

		// IDeviceNotify
		virtual void OnDeviceLost();
		virtual void OnDeviceRestored();

	private:
        // Render depending functionality.
		void ProcessInput();
		void Update();
		bool Render();

        // Project depending functionality.
        void LoadProject();

		// Cached pointer to device resources.
		std::shared_ptr<DX::DeviceResources> m_deviceResources;

		// TODO: Replace with your own content renderers.
		//std::unique_ptr<Sample3DSceneRenderer> m_sceneRenderer;
		std::unique_ptr<SampleFpsTextRenderer> m_fpsTextRenderer;
        std::unique_ptr<Basic2DRenderer> m_basic2dRenderer;

		Windows::Foundation::IAsyncAction^ m_renderLoopWorker;
		Concurrency::critical_section m_criticalSection;

		// Rendering loop timer.
		DX::StepTimer m_timer;

		// Track current input pointer position.
		float m_pointerLocationX;

        Platform::String^ m_projectName;

        bool m_loadingComplete;

        PlayerState m_playerState;

        Windows::UI::Xaml::Controls::CommandBar^ m_playerAppBar;
	};

    
};