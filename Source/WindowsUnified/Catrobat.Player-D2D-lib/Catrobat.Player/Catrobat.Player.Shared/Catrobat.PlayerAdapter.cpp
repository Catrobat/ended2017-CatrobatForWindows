#include "pch.h"
#include "Catrobat.PlayerAdapter.h"

using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Graphics::Display;
using namespace Windows::System::Threading;
using namespace Windows::UI::Core;
using namespace Windows::UI::Input;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::Phone::UI::Input;
using namespace concurrency;
using namespace D2D1;

namespace Catrobat_Player
{
    //----------------------------------------------------------------------

    Catrobat_PlayerAdapter::Catrobat_PlayerAdapter() :
    m_windowVisible(true),
    m_coreInput(nullptr)
    {

    }

    //----------------------------------------------------------------------

    Catrobat_PlayerAdapter::~Catrobat_PlayerAdapter()
    {
        // Stop rendering and processing events on destruction.
        critical_section::scoped_lock lock(m_main->GetCriticalSection());
        m_main->StopRenderLoop();
        m_coreInput->Dispatcher->StopProcessEvents();
    }
    
    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::InitPlayer(Windows::UI::Xaml::Controls::SwapChainPanel^ swapChainPanel,
        Windows::UI::Xaml::Controls::CommandBar^ playerAppBar, 
        Platform::String^ projectName)
    {

        // Register event handlers for page lifecycle.
        CoreWindow^ window = Window::Current->CoreWindow;

        window->VisibilityChanged +=
            ref new TypedEventHandler<CoreWindow^, VisibilityChangedEventArgs^>(this, &Catrobat_PlayerAdapter::OnVisibilityChanged);

        DisplayInformation^ currentDisplayInformation = DisplayInformation::GetForCurrentView();

        currentDisplayInformation->DpiChanged +=
            ref new TypedEventHandler<DisplayInformation^, Object^>(this, &Catrobat_PlayerAdapter::OnDpiChanged);

        currentDisplayInformation->OrientationChanged +=
            ref new TypedEventHandler<DisplayInformation^, Object^>(this, &Catrobat_PlayerAdapter::OnOrientationChanged);

        DisplayInformation::DisplayContentsInvalidated +=
            ref new TypedEventHandler<DisplayInformation^, Object^>(this, &Catrobat_PlayerAdapter::OnDisplayContentsInvalidated);
        
        swapChainPanel->CompositionScaleChanged +=
            ref new TypedEventHandler<SwapChainPanel^, Object^>(this, &Catrobat_PlayerAdapter::OnCompositionScaleChanged);

        swapChainPanel->SizeChanged +=
            ref new SizeChangedEventHandler(this, &Catrobat_PlayerAdapter::OnSwapChainPanelSizeChanged);

        // At this point we have access to the device. 
        // We can create the device-dependent resources.
        m_deviceResources = std::make_shared<DX::DeviceResources>();
        m_deviceResources->SetSwapChainPanel(swapChainPanel);

        // Register our SwapChainPanel to get independent input pointer events
        auto workItemHandler = ref new WorkItemHandler([this, swapChainPanel](IAsyncAction ^)
        {
            // The CoreIndependentInputSource will raise pointer events for the specified device types on whichever thread it's created on.
            m_coreInput = swapChainPanel->CreateCoreIndependentInputSource(
                Windows::UI::Core::CoreInputDeviceTypes::Mouse |
                Windows::UI::Core::CoreInputDeviceTypes::Touch |
                Windows::UI::Core::CoreInputDeviceTypes::Pen
                );

            // Register for pointer events, which will be raised on the background thread.
            m_coreInput->PointerPressed += ref new TypedEventHandler<Object^, PointerEventArgs^>(this, &Catrobat_PlayerAdapter::OnPointerPressed);

            // Begin processing input messages as they're delivered.
            m_coreInput->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessUntilQuit);
        });

        // Run task on a dedicated high priority background thread.
        m_inputLoopWorker = ThreadPool::RunAsync(workItemHandler, WorkItemPriority::High, WorkItemOptions::TimeSliced); 

        m_main = std::unique_ptr<Catrobat_PlayerMain>(new Catrobat_PlayerMain(m_deviceResources, playerAppBar, projectName));
        m_main->StartRenderLoop();
    }

    //----------------------------------------------------------------------
    // Saves the current state of the app for suspend and terminate events.

    void Catrobat_PlayerAdapter::SaveInternalState(Windows::Foundation::Collections::IPropertySet^ state)
    {
        // TODO review, especially check state handling --> maybe create a function for this inside the main class of the player
        critical_section::scoped_lock lock(m_main->GetCriticalSection());
        m_deviceResources->Trim();

        // Stop rendering when the app is suspended.
        m_main->StopRenderLoop();

        // TODO Put code to save app state here.
    }

    //----------------------------------------------------------------------
    // Loads the current state of the app for resume events.

    void Catrobat_PlayerAdapter::LoadInternalState(Windows::Foundation::Collections::IPropertySet^ state)
    {
        // TODO Put code to load app state here.

        // TODO review, especially check state handling --> maybe create a function for this inside the main class of the player
        // Start rendering when the app is resumed.
        m_main->StartRenderLoop();
    }

    //----------------------------------------------------------------------
    // Window event handlers.

    void Catrobat_PlayerAdapter::OnVisibilityChanged(Windows::UI::Core::CoreWindow^ sender, Windows::UI::Core::VisibilityChangedEventArgs^ args)
    {
        // TODO review, especially check state handling --> maybe create a function for this inside the main class of the player
        m_windowVisible = args->Visible;
        if (m_windowVisible)
        {
            m_main->StartRenderLoop();
        }
        else
        {
            m_main->StopRenderLoop();
        }
    }

    //----------------------------------------------------------------------
    // DisplayInformation event handlers.

    void Catrobat_PlayerAdapter::OnDpiChanged(DisplayInformation^ sender, Object^ args)
    {
        critical_section::scoped_lock lock(m_main->GetCriticalSection());
        m_deviceResources->SetDpi(sender->LogicalDpi);
        m_main->CreateWindowSizeDependentResources();
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::OnOrientationChanged(DisplayInformation^ sender, Object^ args)
    {
        critical_section::scoped_lock lock(m_main->GetCriticalSection());
        m_deviceResources->SetCurrentOrientation(sender->CurrentOrientation);
        m_main->CreateWindowSizeDependentResources();
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::OnDisplayContentsInvalidated(DisplayInformation^ sender, Object^ args)
    {
        critical_section::scoped_lock lock(m_main->GetCriticalSection());
        m_deviceResources->ValidateDevice();
    }

    //----------------------------------------------------------------------
    // Other event handlers.

    void Catrobat_PlayerAdapter::OnCompositionScaleChanged(Windows::UI::Xaml::Controls::SwapChainPanel^ sender, Object^ args)
    {
        critical_section::scoped_lock lock(m_main->GetCriticalSection());
        m_deviceResources->SetCompositionScale(sender->CompositionScaleX, sender->CompositionScaleY);
        m_main->CreateWindowSizeDependentResources();
    }


    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::OnSwapChainPanelSizeChanged(Platform::Object^ sender, Windows::UI::Xaml::SizeChangedEventArgs^ e)
    {
        critical_section::scoped_lock lock(m_main->GetCriticalSection());
        m_deviceResources->SetLogicalSize(e->NewSize);
        m_main->CreateWindowSizeDependentResources();
    }

    //----------------------------------------------------------------------
    // Independent input handling functions.

    bool Catrobat_PlayerAdapter::HardwareBackButtonPressed()
    {
        return m_main->HardwareBackButtonPressed();
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::OnPointerPressed(Object^ sender, PointerEventArgs^ e)
    {
        m_main->PointerPressed(Point2F(e->CurrentPoint->Position.X, e->CurrentPoint->Position.Y));
    }

    //----------------------------------------------------------------------
    // Bottom CommandBar handlers

    void Catrobat_PlayerAdapter::RestartButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        m_main->RestartButtonClicked(sender, args);
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::PlayButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        m_main->PlayButtonClicked(sender, args);
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::ThumbnailButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        m_main->ThumbnailButtonClicked(sender, args);
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::EnableAxisButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        m_main->EnableAxisButtonClicked(sender, args);
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::ScreenshotButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
    {
        m_main->ScreenshotButtonClicked(sender, args);
    }
};