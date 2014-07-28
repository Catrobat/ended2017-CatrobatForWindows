//
// Player.xaml.h
// Declaration of the Player class
//

#pragma once

#include "Player.g.h"
#include "PlayerWindowsPhone8Component.h"

using namespace PhoneDirect3DXamlAppComponent;

namespace Catrobat_Player
{
    /// <summary>
    /// A page that hosts a DirectX SwapChainPanel.
    /// </summary>
    [Windows::Foundation::Metadata::WebHostHidden]
    public ref class Player sealed 
    {
    public:
        Player();

        void OnSuspending();
        void OnResuming();

    private:

        // DisplayInformation event handlers.
        //void OnOrientationChanged(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);

        // Bottom CommandBar handlers
        void OnRefreshButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void OnPausePlayButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void OnScreenshotButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);
        void OnEnableAxisButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args);

    private:
        // Resources used to render the DirectX content in the XAML page background.
        std::unique_ptr<Direct3DBackground> m_main;

        bool m_playActive;
    };
}
