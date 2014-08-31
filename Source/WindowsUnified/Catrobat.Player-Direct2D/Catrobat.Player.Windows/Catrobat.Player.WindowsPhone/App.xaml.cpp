///
/// App.xaml.cpp
/// Implementation of the App class.
///

#include "pch.h"
#include "App.xaml.h"

using namespace Platform;
using namespace Catrobat_Player;
using namespace Windows::ApplicationModel;
using namespace Windows::ApplicationModel::Activation;
using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml;


//----------------------------------------------------------------------

App::App()
{
    InitializeComponent();
    Suspending += ref new SuspendingEventHandler(this, &App::OnSuspending);
    Resuming += ref new EventHandler<Object^>(this, &App::OnResuming);

#if defined(_DEBUG)
    UnhandledException += ref new UnhandledExceptionEventHandler([](Object^ /* sender */, UnhandledExceptionEventArgs^ args)
    {
        String^ error = "Catrobat_Player::App::UnhandledException: " + args->Message + "\n";
        OutputDebugStringW(error->Data());
    });
#endif
}

//----------------------------------------------------------------------

void App::OnLaunched(_In_ LaunchActivatedEventArgs^ /* args */)
{
    m_playerDirectXPage = ref new PlayerDirectXPage();

    Window::Current->Content = m_playerDirectXPage;
    Window::Current->Activate();
}

//----------------------------------------------------------------------

void App::OnSuspending(
    _In_ Object^ /* sender */,
    _In_ SuspendingEventArgs^ args
    )
{
    m_playerDirectXPage->OnSuspending();
}

//----------------------------------------------------------------------

void App::OnResuming(
    _In_ Object^ /* sender */,
    _In_ Object^ /* args */
    )
{
    m_playerDirectXPage->OnResuming();
}

//----------------------------------------------------------------------