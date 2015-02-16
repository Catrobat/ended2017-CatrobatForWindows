#pragma once

#include "Common\StepTimer.h"
#include "Common\DeviceResources.h"
#include "Content\SampleFpsTextRenderer.h"
#include "Content\Basic2DRenderer.h"

// Renders Direct2D (and 3D) content on the screen
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
                            Windows::UI::Xaml::Controls::Page^ playerPage, 
                            Platform::String^ projectName);
		~Catrobat_PlayerMain();
		void CreateWindowSizeDependentResources();
        Concurrency::critical_section& GetCriticalSection() { return m_criticalSection; }
        
        // Render functionality
		void StartRenderLoop();
		void StopRenderLoop();
		
        // Event input from the user
        void PointerPressed(D2D1_POINT_2F point);
        bool HardwareBackButtonPressed();
        void RestartButtonClicked();
        void ResumeButtonClicked();
        void ThumbnailButtonClicked();
        void AxisButtonClicked();
        void ScreenshotButtonClicked();

		// IDeviceNotify
		virtual void OnDeviceLost();
		virtual void OnDeviceRestored();

        // Template to get a child of a XAML page
        template <class T>
        static T^ FindChildControl(
            Windows::UI::Xaml::DependencyObject^ control,
            const wchar_t* childName);

        static bool TestStatic() { return true; };

	private:
        // Render depending functionality
		void ProcessInput();
		void Update();
		bool Render();

        // Project depending functionality
        void LoadProject(Platform::String^ projectName);
        void SetAxisValues();

    private:
		// Cached pointer to device resources
		std::shared_ptr<DX::DeviceResources> m_deviceResources;

		// TODO: Replace with your own content renderers
		//std::unique_ptr<Sample3DSceneRenderer> m_sceneRenderer;
		std::unique_ptr<SampleFpsTextRenderer> m_fpsTextRenderer;
        std::unique_ptr<Basic2DRenderer> m_basic2dRenderer;

		Windows::Foundation::IAsyncAction^ m_renderLoopWorker;
		Concurrency::critical_section m_criticalSection;

		// Rendering loop timer
		DX::StepTimer m_timer;

		// Track current input pointer position
		float m_pointerLocationX;

        // Project dependenging member variables
        Windows::UI::Xaml::Controls::CommandBar^ m_appBar;
        Windows::UI::Xaml::Controls::AppBarButton^ m_btnAxis;
        Windows::UI::Xaml::Controls::Grid^ m_gridAxis;
        PlayerState m_state;
        bool m_loadingComplete;
        bool m_axisOn;

        // Constants of the Player's XAML page
        const int m_btnAxisPosition = 3;
        const wchar_t* m_gridAxisXAMLName         = L"GridAxis";
        const wchar_t* m_gridAxisXRightXAMLName   = L"GridAxisXRight";
        const wchar_t* m_gridAxisXLeftXAMLName    = L"GridAxisXLeft";
        const wchar_t* m_gridAxisYTopXAMLName     = L"GridAxisYTop";
        const wchar_t* m_gridAxisYBottomXAMLName  = L"GridAxisYBottom";
	};
};