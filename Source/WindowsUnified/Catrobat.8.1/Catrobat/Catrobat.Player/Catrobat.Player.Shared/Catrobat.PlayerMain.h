#pragma once

#include "Common\StepTimer.h"
#include "Common\DeviceResources.h"
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
		void AxesButtonClicked(bool showAxes, Platform::String^ label);
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
		void LoadProject(Platform::String^ projectName,
			Windows::UI::Xaml::Controls::Page^ playerPage);
		void ProcessXamlPageContent(Windows::UI::Xaml::Controls::Page^ playerPage);

	private:
		// Cached pointer to device resources
		std::shared_ptr<DX::DeviceResources> m_deviceResources;

		std::unique_ptr<Basic2DRenderer> m_basic2dRenderer;

		Windows::Foundation::IAsyncAction^ m_renderLoopWorker;
		Concurrency::critical_section m_criticalSection;

		// Rendering loop timer
		DX::StepTimer m_timer;

		// Track current input pointer position
		float m_pointerLocationX;

		// Project dependenging member variables
		Windows::UI::Xaml::Controls::CommandBar^ m_appBar;
		Windows::UI::Xaml::Controls::AppBarButton^ m_btnAxes;
		Windows::UI::Xaml::Controls::Grid^ m_gridAxes;
		PlayerState m_state;
	};
};