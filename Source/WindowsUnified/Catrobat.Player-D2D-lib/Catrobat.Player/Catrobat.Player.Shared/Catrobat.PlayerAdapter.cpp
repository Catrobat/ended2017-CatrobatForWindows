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

    }
    
    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::InitPlayer(Windows::UI::Xaml::Controls::SwapChainPanel^ swapChainPanel,
        Windows::UI::Xaml::Controls::CommandBar^ playerAppBar)
    {
        /* from DirectXPage.xaml.cpp
        m_windowVisible(true),
        m_coreInput(nullptr)
        {
        InitializeComponent();

        // Register event handlers for page lifecycle.
        CoreWindow^ window = Window::Current->CoreWindow;

        window->VisibilityChanged +=
        ref new TypedEventHandler<CoreWindow^, VisibilityChangedEventArgs^>(this, &DirectXPage::OnVisibilityChanged);

        DisplayInformation^ currentDisplayInformation = DisplayInformation::GetForCurrentView();

        currentDisplayInformation->DpiChanged +=
        ref new TypedEventHandler<DisplayInformation^, Object^>(this, &DirectXPage::OnDpiChanged);

        currentDisplayInformation->OrientationChanged +=
        ref new TypedEventHandler<DisplayInformation^, Object^>(this, &DirectXPage::OnOrientationChanged);

        DisplayInformation::DisplayContentsInvalidated +=
        ref new TypedEventHandler<DisplayInformation^, Object^>(this, &DirectXPage::OnDisplayContentsInvalidated);
        */
        swapChainPanel->CompositionScaleChanged +=
            ref new TypedEventHandler<SwapChainPanel^, Object^>(this, &Catrobat_PlayerAdapter::OnCompositionScaleChanged);

        //swapChainPanel->SizeChanged +=
        //    ref new SizeChangedEventHandler(this, &Catrobat_PlayerAdapter::OnSwapChainPanelSizeChanged);

        // At this point we have access to the device. 
        // We can create the device-dependent resources.
        m_deviceResources = std::make_shared<DX::DeviceResources>();
        m_deviceResources->SetSwapChainPanel(swapChainPanel);

        // Register our SwapChainPanel to get independent input pointer events
        auto workItemHandler = ref new WorkItemHandler([this](IAsyncAction ^)
        {
            // The CoreIndependentInputSource will raise pointer events for the specified device types on whichever thread it's created on.
            //m_coreInput = swapChainPanel->CreateCoreIndependentInputSource(
            //    Windows::UI::Core::CoreInputDeviceTypes::Mouse |
            //    Windows::UI::Core::CoreInputDeviceTypes::Touch |
            //    Windows::UI::Core::CoreInputDeviceTypes::Pen
            //    );
            // TODO comment this block in & check why it is not working

            // Register for pointer events, which will be raised on the background thread.
            m_coreInput->PointerPressed += ref new TypedEventHandler<Object^, PointerEventArgs^>(this, &Catrobat_PlayerAdapter::PointerPressed);

            // Begin processing input messages as they're delivered.
            m_coreInput->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessUntilQuit);
        });

        // Run task on a dedicated high priority background thread.
        //m_inputLoopWorker = ThreadPool::RunAsync(workItemHandler, WorkItemPriority::High, WorkItemOptions::TimeSliced);    TODO activate this code

        m_main = std::unique_ptr<Catrobat_PlayerMain>(new Catrobat_PlayerMain(m_deviceResources, playerAppBar));
        m_main->StartRenderLoop();
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::OnCompositionScaleChanged(Windows::UI::Xaml::Controls::SwapChainPanel^ sender, Object^ args)
    {
        critical_section::scoped_lock lock(m_main->GetCriticalSection());
        m_deviceResources->SetCompositionScale(sender->CompositionScaleX, sender->CompositionScaleY);
        m_main->CreateWindowSizeDependentResources();
    }

    ////----------------------------------------------------------------------

    //void Catrobat_PlayerAdapter::OnSwapChainPanelSizeChanged(Object^ sender, SizeChangedEventArgs^ e)
    //{
    //    critical_section::scoped_lock lock(m_main->GetCriticalSection());
    //    m_deviceResources->SetLogicalSize(e->NewSize);
    //    m_main->CreateWindowSizeDependentResources();
    //}

    //----------------------------------------------------------------------

    bool Catrobat_PlayerAdapter::HardwareBackButtonPressed()
    {
        return m_main->HardwareBackButtonPressed();
    }

    //----------------------------------------------------------------------

    void Catrobat_PlayerAdapter::PointerPressed(Object^ sender, PointerEventArgs^ e)
    {
        m_main->PointerPressed(Point2F(e->CurrentPoint->Position.X, e->CurrentPoint->Position.Y));
    }

    //----------------------------------------------------------------------

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