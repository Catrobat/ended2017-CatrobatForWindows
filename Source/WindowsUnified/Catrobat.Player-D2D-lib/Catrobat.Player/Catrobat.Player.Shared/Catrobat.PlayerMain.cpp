#include "pch.h"
#include "Catrobat.PlayerMain.h"
#include "Common\DirectXHelper.h"
#include "ProjectDaemon.h"

using namespace Catrobat_Player;
using namespace Windows::Foundation;
using namespace Windows::System::Threading;
using namespace Windows::Phone::UI::Input;
using namespace Windows::UI::Notifications;
using namespace Windows::Data::Xml::Dom;
using namespace Concurrency;

//----------------------------------------------------------------------
// Loads and initializes application assets when the application is loaded.

Catrobat_PlayerMain::Catrobat_PlayerMain(const std::shared_ptr<DX::DeviceResources>& deviceResources,
                                         Windows::UI::Xaml::Controls::CommandBar^ playerAppBar,
                                         Platform::String^ projectName) :
m_deviceResources(deviceResources), 
m_pointerLocationX(0.0f),
m_loadingComplete(false),
m_playerAppBar(playerAppBar),
m_playerState(PlayerState::Init),
m_projectName(projectName)
{
    // Register to be notified if the Device is lost or recreated
    m_deviceResources->RegisterDeviceNotify(this);
    
    LoadProject();
    m_fpsTextRenderer = std::unique_ptr<SampleFpsTextRenderer>(new SampleFpsTextRenderer(m_deviceResources));

    // TODO: Change the timer settings if you want something other than the default variable timestep mode.
    // e.g. for 60 FPS fixed timestep update logic, call:
    /*
    m_timer.SetFixedTimeStep(true);
    m_timer.SetTargetElapsedSeconds(1.0 / 60);
    */
}

//----------------------------------------------------------------------

Catrobat_PlayerMain::~Catrobat_PlayerMain()
{
    // Deregister device notification
    m_deviceResources->RegisterDeviceNotify(nullptr);
}

//----------------------------------------------------------------------
// Initialize Project loading and parsing.

void Catrobat_PlayerMain::LoadProject()
{
    ProjectDaemon::Instance()->OpenProject(m_projectName).then([this](task<bool> t)
    {
        m_basic2dRenderer = std::unique_ptr<Basic2DRenderer>(new Basic2DRenderer(m_deviceResources));
        m_loadingComplete = true;
        m_playerState = PlayerState::Active;
    });
  
}

//----------------------------------------------------------------------
// Updates application state when the window size changes (e.g. device orientation change)

void Catrobat_PlayerMain::CreateWindowSizeDependentResources()
{
    // TODO: Replace this with the size-dependent initialization of your app's content.
    m_basic2dRenderer->CreateWindowSizeDependentResources();
}

//----------------------------------------------------------------------

void Catrobat_PlayerMain::StartRenderLoop()
{
    // If the animation render loop is already running then do not start another thread.
    if (m_renderLoopWorker != nullptr && m_renderLoopWorker->Status == Windows::Foundation::AsyncStatus::Started)
    {
        return;
    }

    // Create a task that will be run on a background thread.
    auto workItemHandler = ref new WorkItemHandler([this](Windows::Foundation::IAsyncAction ^ action)
    {
        // Calculate the updated frame and render once per vertical blanking interval.
        while (action->Status == Windows::Foundation::AsyncStatus::Started)
        {
            critical_section::scoped_lock lock(m_criticalSection);
            Update();
            if (Render())
            {
                m_deviceResources->Present();
            }
        }
    });

    // Run task on a dedicated high priority background thread.
    m_renderLoopWorker = ThreadPool::RunAsync(workItemHandler, WorkItemPriority::High, WorkItemOptions::TimeSliced);
}

//----------------------------------------------------------------------

void Catrobat_PlayerMain::StopRenderLoop()
{
    m_playerState = PlayerState::Pause;
    m_renderLoopWorker->Cancel();
}

//----------------------------------------------------------------------

void Catrobat_PlayerMain::PointerPressed(D2D1_POINT_2F point)
{ 
    if (m_playerState == PlayerState::Active)
    { 
        m_basic2dRenderer->PointerPressed(point); 
    }
}

//----------------------------------------------------------------------

bool Catrobat_PlayerMain::HardwareBackButtonPressed()
{
    if (m_playerState == PlayerState::Active)
    {
        // Player is in active play mode, so hitting the hardware back button
        // will cause the game to pause and show the user the command bar --> return false

        critical_section::scoped_lock lock(m_criticalSection);

        StopRenderLoop();
        m_playerState = PlayerState::Pause;
        m_playerAppBar->Visibility = Windows::UI::Xaml::Visibility::Visible;
        return false;
    }
    else
    {
        // Player is in pause or init mode, so hitting the hardware back button
        // will cause the player to terminate and bring the user back to the IDE --> return true
        
        return true;
    }
}

//----------------------------------------------------------------------

void Catrobat_PlayerMain::RestartButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    critical_section::scoped_lock lock(m_criticalSection);
    m_playerState = PlayerState::Init;

    ProjectDaemon::Instance()->RestartProject();
    m_playerAppBar->Visibility = Windows::UI::Xaml::Visibility::Collapsed;
    m_playerState = PlayerState::Active;
    
    StartRenderLoop();  
}
                                                  
//----------------------------------------------------------------------

void Catrobat_PlayerMain::PlayButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    critical_section::scoped_lock lock(m_criticalSection);

    m_playerAppBar->Visibility = Windows::UI::Xaml::Visibility::Collapsed;
    m_playerState = PlayerState::Active;
    StartRenderLoop();
}

//----------------------------------------------------------------------
// 
void Catrobat_PlayerMain::ThumbnailButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    // TODO get current screen image
    // TODO save the current image

    // Create a template for the toast 
    ToastTemplateType toastTemplate = ToastTemplateType::ToastText02;
    XmlDocument^ toastXml = ToastNotificationManager::GetTemplateContent(toastTemplate);

    // 
    XmlNodeList^ toastTextElements = toastXml->GetElementsByTagName("text");
    toastTextElements->Item(0)->InnerText = "Successfully set a new thumbnail :)"; // TODO get this string from a multilingual tool/file
    ToastNotification^ toast = ref new ToastNotification(toastXml);
    Platform::String^ toastTag = ref new Platform::String(L"Thumbnail"); // TODO check if this should maybe set somewhere in a global variable space??
    toast->Tag = toastTag; 

    // Show the toast and remove it from the action center
    int seconds = 1;
    auto cal = ref new Windows::Globalization::Calendar();
    cal->AddSeconds(seconds);
    toast->ExpirationTime = cal->GetDateTime();
    ToastNotificationManager::CreateToastNotifier()->Show(toast);
    //ToastNotificationManager::History->Remove(toastTag);
    
    critical_section::scoped_lock lock(m_criticalSection);
    m_playerAppBar->Visibility = Windows::UI::Xaml::Visibility::Collapsed;
    m_playerState = PlayerState::Active;
    StartRenderLoop();
}

//----------------------------------------------------------------------

void Catrobat_PlayerMain::EnableAxisButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    // TODO implement me
}

//----------------------------------------------------------------------

void Catrobat_PlayerMain::ScreenshotButtonClicked(_In_ Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ args)
{
    // TODO implement me
}

//----------------------------------------------------------------------
// Updates the application state once per frame.
 
void Catrobat_PlayerMain::Update()
{
    ProcessInput();

    // Update scene objects.
    m_timer.Tick([&]()
    {
        // TODO: Replace this with your app's content update functions.
        if (m_loadingComplete)
        {
            m_basic2dRenderer->Update(m_timer);
        }
        m_fpsTextRenderer->Update(m_timer);
    });
}

//----------------------------------------------------------------------
// Process all input from the user before updating game state

void Catrobat_PlayerMain::ProcessInput()
{
    // TODO: Add per frame input handling here.
    //m_sceneRenderer->TrackingUpdate(m_pointerLocationX);
}

//----------------------------------------------------------------------
// Renders the current frame according to the current application state.
// Returns true if the frame was rendered and is ready to be displayed.

bool Catrobat_PlayerMain::Render()
{
    // Don't try to render anything before the first Update.
    if (m_timer.GetFrameCount() == 0)
    {
        return false;
    }

    auto context = m_deviceResources->GetD3DDeviceContext();

    // Reset the viewport to target the whole screen.
    auto viewport = m_deviceResources->GetScreenViewport();
    context->RSSetViewports(1, &viewport);

    // Reset render targets to the screen.
    ID3D11RenderTargetView *const targets[1] = { m_deviceResources->GetBackBufferRenderTargetView() };
    context->OMSetRenderTargets(1, targets, m_deviceResources->GetDepthStencilView());

    // Clear the back buffer and depth stencil view.
    context->ClearRenderTargetView(m_deviceResources->GetBackBufferRenderTargetView(), DirectX::Colors::CornflowerBlue);
    context->ClearDepthStencilView(m_deviceResources->GetDepthStencilView(), D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL, 1.0f, 0);

    // Render the scene objects.
    // TODO: Replace this with your app's content rendering functions.
    if (m_loadingComplete)
    {
        m_basic2dRenderer->Render();
    }
    m_fpsTextRenderer->Render();

    return true;
}

//----------------------------------------------------------------------
// Notifies renderers that device resources need to be released.

void Catrobat_PlayerMain::OnDeviceLost()
{
    if (m_loadingComplete)
    {
        m_basic2dRenderer->ReleaseDeviceDependentResources();
    }
    m_fpsTextRenderer->ReleaseDeviceDependentResources();
}


//----------------------------------------------------------------------
// Notifies renderers that device resources may now be recreated.

void Catrobat_PlayerMain::OnDeviceRestored()
{
    if (m_loadingComplete)
    {
        m_basic2dRenderer->CreateDeviceDependentResources();
    }
    m_fpsTextRenderer->CreateDeviceDependentResources();
    CreateWindowSizeDependentResources();
}
