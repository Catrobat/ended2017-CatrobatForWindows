////
//// PlayerDirectXPage.xaml.cpp
//// Implementation of the PlayerDirectXPage class
////

#include "pch.h"
#include "App.xaml.h"
#include "PlayerDirectXPage.xaml.h"

using namespace Catrobat_Player;

using namespace Microsoft::WRL;                     // c-block
using namespace Platform;
using namespace Windows::ApplicationModel;

using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;    // c
using namespace Windows::Graphics::Display;
using namespace Windows::Storage;           // c
using namespace Windows::UI::Core;
using namespace Windows::UI::Input;
using namespace Windows::UI::ViewManagement;   // c
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;

using namespace Windows::UI::Xaml::Data;    // c-block
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::ApplicationModel::Store;
using namespace Windows::UI::Popups;

using namespace concurrency;

    //----------------------------------------------------------------------

    PlayerDirectXPage::PlayerDirectXPage()
        //m_playActive(true)
    {
        InitializeComponent();

        // Register event handlers for page lifecycle.
        CoreWindow^ window = Window::Current->CoreWindow;

        window->VisibilityChanged +=
            ref new TypedEventHandler<CoreWindow^, VisibilityChangedEventArgs^>(this, &PlayerDirectXPage::OnVisibilityChanged);
        window->SizeChanged +=
            ref new TypedEventHandler<CoreWindow^, WindowSizeChangedEventArgs^>(this, &PlayerDirectXPage::OnWindowSizeChanged);

        Window::Current->Activated +=
            ref new WindowActivatedEventHandler(this, &PlayerDirectXPage::OnWindowActivationChanged);

        DisplayInformation^ currentDisplayInformation = DisplayInformation::GetForCurrentView();

        currentDisplayInformation->DpiChanged +=
            ref new TypedEventHandler<DisplayInformation^, Object^>(this, &PlayerDirectXPage::OnDpiChanged);

        currentDisplayInformation->OrientationChanged +=
            ref new TypedEventHandler<DisplayInformation^, Object^>(this, &PlayerDirectXPage::OnOrientationChanged);

        DisplayInformation::DisplayContentsInvalidated +=
            ref new TypedEventHandler<DisplayInformation^, Object^>(this, &PlayerDirectXPage::OnDisplayContentsInvalidated);

        PlayerSwapChainPanel->CompositionScaleChanged +=
            ref new TypedEventHandler<SwapChainPanel^, Object^>(this, &PlayerDirectXPage::OnCompositionScaleChanged);

        PlayerSwapChainPanel->SizeChanged +=
            ref new SizeChangedEventHandler(this, &PlayerDirectXPage::OnSwapChainPanelSizeChanged);

#if WINAPI_FAMILY != WINAPI_FAMILY_PHONE_APP
        // Disable all pointer visual feedback for better performance when touching.
        // PointerVisualizationSettings are not implemented on WindowsPhone.
        auto pointerVisualizationSettings = PointerVisualizationSettings::GetForCurrentView();
        pointerVisualizationSettings->IsContactFeedbackEnabled = false;
        pointerVisualizationSettings->IsBarrelButtonFeedbackEnabled = false;
#endif

        // At this point we have access to the device.
        // We can create the device-dependent resources.
        m_direct3DDeviceResources = std::make_shared<DX::Direct3DDeviceResources>();
        m_direct3DDeviceResources->SetSwapChainPanel(PlayerSwapChainPanel);

        m_playerMainComponent = std::unique_ptr<PlayerMainComponent>(new PlayerMainComponent(m_direct3DDeviceResources, PlayerAppBar));
        m_playerMainComponent->StartRenderLoop();

        //m_main->ProjectName = "Default";
        //m_main->RenderResolution = m_main->NativeResolution;
        //m_main->StartRenderLoop();
    }

    // Window event handlers.
    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnVisibilityChanged(CoreWindow^ sender, VisibilityChangedEventArgs^ args)
    {
        OutputDebugString(L"OnSwapChainPanelSizeChanged\n");
        m_windowVisible = args->Visible;
        if (m_windowVisible)
        {
            m_playerMainComponent->StartRenderLoop();
        }
        else
        {
            m_playerMainComponent->StopRenderLoop();
        }
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnWindowActivationChanged(
        _In_ Platform::Object^ /* sender */,
        _In_ Windows::UI::Core::WindowActivatedEventArgs^ args
        )
    {
        OutputDebugString(L"OnWindowActivationChanged\n");
        critical_section::scoped_lock lock(m_playerMainComponent->GetCriticalSection());
        m_playerMainComponent->WindowActivationChanged(args->WindowActivationState);
    }

    //----------------------------------------------------------------------
    void PlayerDirectXPage::OnWindowSizeChanged(
        _In_ CoreWindow^ /* window */,
        _In_ WindowSizeChangedEventArgs^ /* args */
        )
    {
        OutputDebugString(L"OnWindowSizeChanged\n");
        //StoreGrid->Height = Window::Current->Bounds.Height;
        //StoreFlyout->HorizontalOffset = Window::Current->Bounds.Width - StoreGrid->Width;
    }

    // DisplayInformation event handlers.
    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnDpiChanged(DisplayInformation^ sender, Object^ args)
    {
        OutputDebugString(L"OnDpiChanged\n");
        critical_section::scoped_lock lock(m_playerMainComponent->GetCriticalSection());
        m_direct3DDeviceResources->SetDpi(sender->LogicalDpi);
        m_playerMainComponent->CreateWindowSizeDependentResources();
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnOrientationChanged(DisplayInformation^ sender, Object^ args)
    {
        OutputDebugString(L"OnOrientationChanged\n");
        // when landscape mode is supported --> activate this code

        //critical_section::scoped_lock lock(m_playerMainComponent->GetCriticalSection());
        //m_direct3DDeviceResources->SetCurrentOrientation(sender->CurrentOrientation);
        //m_playerMainComponent->CreateWindowSizeDependentResources();
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnDisplayContentsInvalidated(DisplayInformation^ sender, Object^ args)
    {
        OutputDebugString(L"OnDisplayContentsInvalidated\n");
        // Occurs when the display requires redrawing.

        // TODO activate this code and implement the function ValidateDevice()

        //critical_section::scoped_lock lock(m_playerMainComponent->GetCriticalSection());
        //m_direct3DDeviceResources->ValidateDevice();
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnCompositionScaleChanged(SwapChainPanel^ sender, Object^ args)
    {
        OutputDebugString(L"OnCompositionScaleChanged\n");
        critical_section::scoped_lock lock(m_playerMainComponent->GetCriticalSection());
        m_direct3DDeviceResources->SetCompositionScale(sender->CompositionScaleX, sender->CompositionScaleY);
        m_playerMainComponent->CreateWindowSizeDependentResources();
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnSwapChainPanelSizeChanged(Object^ sender, SizeChangedEventArgs^ e)
    {
        OutputDebugString(L"OnSwapChainPanelSizeChanged\n");
        critical_section::scoped_lock lock(m_playerMainComponent->GetCriticalSection());
        m_direct3DDeviceResources->SetLogicalSize(e->NewSize);
        m_playerMainComponent->CreateWindowSizeDependentResources();
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnSuspending()
    {
        //TODO: implement me

        //critical_section::scoped_lock lock(m_main->GetCriticalSection());
        //m_main->Suspend();
        //// Stop rendering when the app is suspended.
        //m_main->StopRenderLoop();

        //m_deviceResources->Trim();

    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnResuming()
    {
        //TODO: implement me

        //m_main->Resume();
        //m_main->StartRenderLoop();
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnRestartButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        //TODO: implement me
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnPausePlayButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        //TODO: implement me

        //if (m_playActive)
        //{
        //    m_playActive = false;
        //    //m_main->PauseRequested();
        //    PausePlay->Icon = ref new SymbolIcon(Symbol::Play);
        //    PausePlay->Label = "Play";
        //}
        //else
        //{
        //    m_playActive = true;
        //    //m_main->ContinueRequested();
        //    PausePlay->Icon = ref new SymbolIcon(Symbol::Pause);
        //    PausePlay->Label = "Pause";
        //}
    }


    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnScreenshotButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        //TODO: implement me
    }

    //----------------------------------------------------------------------

    void PlayerDirectXPage::OnEnableAxisButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        //TODO: implement me
    }
