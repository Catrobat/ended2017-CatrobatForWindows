//
// Player.xaml.h
// Declaration of the Player class
//

#pragma once

#include "PlayerDirectXPage.g.h"
#include "PlayerMainComponent.h"

//using namespace PhoneDirect3DXamlAppComponent;

namespace Catrobat_Player
{
    /// <summary>
    /// A page that hosts a DirectX SwapChainPanel.
    /// </summary>
    [Windows::Foundation::Metadata::WebHostHidden]
    public ref class PlayerDirectXPage sealed 
    {
    public:
        PlayerDirectXPage();

        void SetProjectLoading();
        void OnSuspending();
        void OnResuming();

    private:
        // Window event handlers.
        void OnVisibilityChanged(Windows::UI::Core::CoreWindow^ sender, Windows::UI::Core::VisibilityChangedEventArgs^ args);
        void OnWindowActivationChanged(
            _In_ Platform::Object^ sender,
            _In_ Windows::UI::Core::WindowActivatedEventArgs^ args
            );

        // DisplayInformation event handlers.
        void OnDpiChanged(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);
        void OnOrientationChanged(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);
        void OnDisplayContentsInvalidated(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);

        // Other event handlers.
        void OnCompositionScaleChanged(Windows::UI::Xaml::Controls::SwapChainPanel^ sender, Object^ args);
        void OnSwapChainPanelSizeChanged(Platform::Object^ sender, Windows::UI::Xaml::SizeChangedEventArgs^ e);
        void OnWindowSizeChanged(
            _In_ Windows::UI::Core::CoreWindow^ sender,
            _In_ Windows::UI::Core::WindowSizeChangedEventArgs^ args
            );

        // Bottom CommandBar handlers
        void OnRestartButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void OnPausePlayButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void OnScreenshotButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void OnEnableAxisButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);

        // Resources used to render the DirectX content in the XAML page background.
        std::shared_ptr<DX::DeviceResources> m_deviceResources;
        std::unique_ptr<PlayerMainComponent> m_playerMainComponent;

        //bool m_playActive;
        bool m_windowVisible;
    };
}
