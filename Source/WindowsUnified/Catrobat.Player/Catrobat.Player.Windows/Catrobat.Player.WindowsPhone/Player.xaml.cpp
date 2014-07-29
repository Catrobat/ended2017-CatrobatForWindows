///
/// Player.xaml.cpp
/// Implementation of the Player class
///

#include "pch.h"
#include "App.xaml.h"
#include "Player.xaml.h"

using namespace Catrobat_Player;

using namespace Microsoft::WRL;
using namespace Platform;
using namespace Windows::ApplicationModel;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Graphics::Display;
using namespace Windows::Storage;
using namespace Windows::UI::Core;
using namespace Windows::UI::Input;
using namespace Windows::UI::ViewManagement;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::ApplicationModel::Store;
using namespace Windows::UI::Popups;
using namespace concurrency;


//----------------------------------------------------------------------

Player::Player() :
m_playActive(true)
{
    InitializeComponent();


    // Register event handlers for page lifecycle.
    CoreWindow^ window = Window::Current->CoreWindow;

    // At this point we have access to the device.
    // We can create the device-dependent resources.
    m_main = std::unique_ptr<Direct3DBackground>(ref new Direct3DBackground(window));

    m_main->WindowBounds = new Windows::Foundation::Size(
        (float)Window::Current->Bounds.Height,
        (float)Window::Current->Bounds.Width
    );

    m_main->NativeResolution = new Windows::Foundation::Size(
        // TODO: handle also for Store project
        (float)Window::Current->Bounds.Height * DisplayInformation::GetForCurrentView()->RawPixelsPerViewPixel,
        (float)Window::Current->Bounds.Width  * DisplayInformation::GetForCurrentView()->RawPixelsPerViewPixel
        );

    m_main->ProjectName = "Default";

    m_main->RenderResolution = m_main->NativeResolution;

    m_main->StartRenderLoop();
}

//----------------------------------------------------------------------

void Player::OnSuspending()
{
    //TODO: implement me

    //critical_section::scoped_lock lock(m_main->GetCriticalSection());
    //m_main->Suspend();
    //// Stop rendering when the app is suspended.
    //m_main->StopRenderLoop();

    //m_deviceResources->Trim();

}

//----------------------------------------------------------------------

void Player::OnResuming()
{
    //TODO: implement me

    //m_main->Resume();
    //m_main->StartRenderLoop();
}

//----------------------------------------------------------------------

void Player::OnRefreshButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    //TODO: implement me
}

//----------------------------------------------------------------------

void Player::OnPausePlayButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    //TODO: implement me

    if (m_playActive)
    {
        m_playActive = false;
        //m_main->PauseRequested();
        PausePlay->Icon = ref new SymbolIcon(Symbol::Play);
        PausePlay->Label = "Play";
    }
    else
    {
        m_playActive = true;
        //m_main->ContinueRequested();
        PausePlay->Icon = ref new SymbolIcon(Symbol::Pause);
        PausePlay->Label = "Pause";
    }
}


//----------------------------------------------------------------------

void Player::OnScreenshotButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    //TODO: implement me
}

//----------------------------------------------------------------------

void Player::OnEnableAxisButtonClicked(Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    //TODO: implement me
}



