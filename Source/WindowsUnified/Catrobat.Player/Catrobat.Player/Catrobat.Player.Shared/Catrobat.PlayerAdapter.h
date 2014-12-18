#pragma once

#include "Catrobat.PlayerMain.h"
#include "Common\DeviceResources.h"
#include "Content\Basic2DRenderer.h"

namespace Catrobat_Player
{
    [Windows::Foundation::Metadata::WebHostHidden]
    public ref class Catrobat_PlayerAdapter sealed
    {
    public:
        Catrobat_PlayerAdapter();
        virtual ~Catrobat_PlayerAdapter();

        void InitPlayer(Windows::UI::Xaml::Controls::SwapChainPanel^ swapChainPanel,
            Windows::UI::Xaml::Controls::CommandBar^ playerAppBar,
            Platform::String^ projectName);

        void SaveInternalState(Windows::Foundation::Collections::IPropertySet^ state);
        void LoadInternalState(Windows::Foundation::Collections::IPropertySet^ state);

        // Independent input handling functions - public.
        bool HardwareBackButtonPressed();

        // Bottom CommandBar handlers.
        void RestartButtonClicked();
        void PlayButtonClicked();
        void ThumbnailButtonClicked();
        void EnableAxisButtonClicked();
        void ScreenshotButtonClicked();

    private:
        // Window event handlers.
        void OnVisibilityChanged(Windows::UI::Core::CoreWindow^ sender, 
            Windows::UI::Core::VisibilityChangedEventArgs^ args);

        // DisplayInformation event handlers.
        void OnDpiChanged(Windows::Graphics::Display::DisplayInformation^ sender, 
            Platform::Object^ args);
        void OnOrientationChanged(Windows::Graphics::Display::DisplayInformation^ sender, 
            Platform::Object^ args);
        void OnDisplayContentsInvalidated(Windows::Graphics::Display::DisplayInformation^ sender, 
            Platform::Object^ args);

        // Other event handlers.
        void OnCompositionScaleChanged(Windows::UI::Xaml::Controls::SwapChainPanel^ sender, 
            Object^ args);
        void OnSwapChainPanelSizeChanged(Platform::Object^ sender, 
            Windows::UI::Xaml::SizeChangedEventArgs^ e);

        // Independent input handling functions - private.
        void OnPointerPressed(Platform::Object^ sender, Windows::UI::Core::PointerEventArgs^ e);

        // Track our independent input on a background worker thread.
        Windows::Foundation::IAsyncAction^ m_inputLoopWorker;
        Windows::UI::Core::CoreIndependentInputSource^ m_coreInput;

        // Resources used to render the DirectX content in the XAML page background.
        std::shared_ptr<DX::DeviceResources> m_deviceResources;
        std::unique_ptr<Catrobat_PlayerMain> m_main;
        bool m_windowVisible;
    };
};