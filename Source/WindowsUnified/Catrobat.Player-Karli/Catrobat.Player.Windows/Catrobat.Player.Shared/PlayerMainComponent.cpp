//#include "lodepng.h"
//#include "lodepng_util.h"
//#include "DDSLoader.h"
//#include "Interpreter.h"
//#include "XMLParserFatalException.h"
//#include "PlayerException.h"
//
//#include <windows.system.threading.h>
//#include <exception>
//#include <windows.foundation.h>
//#include <thread>
//#include <math.h>

#include "pch.h"
#include "PlayerMainComponent.h"
//#include "XMLParser.h"
//#include "ProjectDaemon.h"
//#include "ScriptHandler.h"
//#include "WhenScript.h"

//using namespace Catrobat_Player;

using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Microsoft::WRL;
//using namespace Windows::Phone::Graphics::Interop;
//using namespace Windows::Phone::Input::Interop;
using namespace Windows::Graphics::Display;
using namespace Windows::System::Threading;
using namespace Platform;

namespace Catrobat_Player
{
    //--------------------------------------------------------------------------------------

    PlayerMainComponent::PlayerMainComponent(const std::shared_ptr<DX::Direct3DDeviceResources>& direct3DDeviceResources,
        Windows::UI::Xaml::Controls::CommandBar^ playerAppBar) :
        m_direct3DDeviceResources(direct3DDeviceResources),
        m_playerAppBar(playerAppBar)
    {
        //      ProjectDaemon::Instance()->ReInit();

        //m_eventController = EventControllerXaml::Create(m_coreWindow, m_swapChainPanel->Dispatcher);

        m_initRenderer = ref new InitRenderer(m_direct3DDeviceResources);
        //m_projectRenderer = ref new ProjectRenderer(m_direct3DDeviceResources);



    }

    //--------------------------------------------------------------------------------------

    PlayerMainComponent::~PlayerMainComponent()
    {
        //m_initialized = false;
    }

    //--------------------------------------------------------------------------------------

    /*void Direct3DBackground::Render()
    {
    StartRenderLoop();
    }*/

    //--------------------------------------------------------------------------------------

    void PlayerMainComponent::StartRenderLoop()
    {
        // TODO: FIXXME

        while (1)
        {
            m_initRenderer->Render();
        }

        // If the animation render loop is already running then do not start another thread.
        //if (m_renderLoopWorker != nullptr && m_renderLoopWorker->Status == Windows::Foundation::AsyncStatus::Started)
        //{
        //    return;
        //}

        // Create a task that will be run on a background thread.
        //auto workItemHandler = ref new WorkItemHandler([this](IAsyncAction ^ action)
        //{
        // Calculate the updated frame and render once per vertical blanking interval.
        //while (action->Status == Windows::Foundation::AsyncStatus::Started)
        //{
        //critical_section::scoped_lock lock(m_criticalSection);
        //Update();

        //m_renderer->Render();
        //m_deviceResources->Present();

        //Draw(_In_ ID3D11Device1* device, _In_ ID3D11DeviceContext1* context, _In_ ID3D11RenderTargetView* renderTargetView)
        //Draw(GetDevice(), GetContext(), nullptr /* GetRenderTargetView() */ );

        //if (!m_haveFocus || (m_updateState == UpdateEngineState::TooSmall))
        //{
        //    // The app is in an inactive state so stop rendering
        //    // This optimizes for power and allows the framework to become more queiecent
        //    break;
        //}
        //    }
        //});

        // Run task on a dedicated high priority background thread.
        //m_renderLoopWorker = ThreadPool::RunAsync(workItemHandler, WorkItemPriority::High, WorkItemOptions::TimeSliced);
    }

    //--------------------------------------------------------------------------------------

    void PlayerMainComponent::StopRenderLoop()
    {
        // TODO: implement me

        //m_renderLoopWorker->Cancel();
    }

    //--------------------------------------------------------------------------------------

    void PlayerMainComponent::CreateWindowSizeDependentResources()
    {
        // TODO: implement me

        //m_initRenderer->CreateWindowSizeDependentResources();

        // The location of the Control regions may have changed with the size change so update the controller.
        //auto bounds = m_direct3DDeviceResources->GetLogicalSize();


        //if (m_renderLoopWorker == nullptr || m_renderLoopWorker->Status != AsyncStatus::Started)
        //{
        //    StartRenderLoop();
        //}
    }

    //--------------------------------------------------------------------------------------

    void PlayerMainComponent::WindowActivationChanged(CoreWindowActivationState activationState)
    {

        // TODO: implement me

        //if (activationState == CoreWindowActivationState::Deactivated)
        //{
        //    m_haveFocus = false;

        //    switch (m_updateState)
        //    {
        //    case UpdateEngineState::Dynamics:
        //        // From Dynamic mode, when coming out of Deactivated rather than going directly back into game play
        //        // go to the paused state waiting for user input to continue
        //        m_updateStateNext = UpdateEngineState::WaitingForPress;
        //        m_pressResult = PressResultState::ContinueLevel;
        //        SetGameInfoOverlay(GameInfoOverlayState::Pause);
        //        m_uiControl->ShowGameInfoOverlay();
        //        m_game->PauseGame();
        //        m_updateState = UpdateEngineState::Deactivated;
        //        m_uiControl->SetAction(GameInfoOverlayCommand::None);
        //        break;

        //    case UpdateEngineState::WaitingForResources:
        //    case UpdateEngineState::WaitingForPress:
        //        m_updateStateNext = m_updateState;
        //        m_updateState = UpdateEngineState::Deactivated;
        //        m_uiControl->SetAction(GameInfoOverlayCommand::None);
        //        m_uiControl->ShowGameInfoOverlay();
        //        break;
        //    }
        //    // Don't have focus so shutdown input processing
        //    m_controller->Active(false);
        //}
        //else if (activationState == CoreWindowActivationState::CodeActivated
        //    || activationState == CoreWindowActivationState::PointerActivated)
        //{
        //    m_haveFocus = true;

        //    if (m_updateState == UpdateEngineState::Deactivated)
        //    {
        //        m_updateState = m_updateStateNext;

        //        if (m_updateState == UpdateEngineState::WaitingForPress)
        //        {
        //            m_uiControl->SetAction(GameInfoOverlayCommand::TapToContinue);
        //            m_controller->WaitForPress();
        //        }
        //        else if (m_updateStateNext == UpdateEngineState::WaitingForResources)
        //        {
        //            m_uiControl->SetAction(GameInfoOverlayCommand::PleaseWait);
        //        }

        //        if (m_renderLoopWorker == nullptr || m_renderLoopWorker->Status != AsyncStatus::Started)
        //        {
        //            StartRenderLoop();
        //        }
        //    }
        //}
    }

    //--------------------------------------------------------------------------------------

    void PlayerMainComponent::Suspend()
    {
        // TODO: implement me

        //// Save application state.
        //switch (m_updateState)
        //{
        //case UpdateEngineState::Dynamics:
        //    // Game is in the active game play state, Stop Game Timer and Pause play and save state
        //    m_uiControl->SetAction(GameInfoOverlayCommand::None);
        //    SetGameInfoOverlay(GameInfoOverlayState::Pause);
        //    m_updateStateNext = UpdateEngineState::WaitingForPress;
        //    m_pressResult = PressResultState::ContinueLevel;
        //    m_game->PauseGame();
        //    break;

        //case UpdateEngineState::WaitingForResources:
        //case UpdateEngineState::WaitingForPress:
        //    m_updateStateNext = m_updateState;
        //    break;

        //default:
        //    // any other state don't save as next state as they are trancient states and have already set m_updateStateNext
        //    break;
        //}
        //m_updateState = UpdateEngineState::Suspended;

        //m_controller->Active(false);
        //m_game->OnSuspending();
    }

    //--------------------------------------------------------------------------------------

    void PlayerMainComponent::Resume()
    {
        // TODO: implement me

        //if (m_haveFocus)
        //{
        //    m_updateState = m_updateStateNext;
        //}
        //else
        //{
        //    m_updateState = UpdateEngineState::Deactivated;
        //}

        //if (m_updateState == UpdateEngineState::WaitingForPress)
        //{
        //    m_uiControl->SetAction(GameInfoOverlayCommand::TapToContinue);
        //    m_controller->WaitForPress();
        //}
        //m_game->OnResuming();

    }

    //--------------------------------------------------------------------------------------

    //    HRESULT Direct3DBackground::Connect(_In_ IDrawingSurfaceRuntimeHostNative* host, _In_ ID3D11Device1* device)
    //    {
    //        // Initialize Renderer
    //        m_renderer = ref new Renderer();
    //        m_renderer->Initialize(device);
    //        m_device = device;
    //
    //        // Initialize Sound
    //        SoundManager::Instance()->Initialize();
    //
    //        //Initialize Project Renderer
    //        m_renderingErrorOccured = false;
    //        m_projectRenderer = ref new ProjectRenderer();
    //        ProjectDaemon::Instance()->SetupRenderer(device, m_projectRenderer);
    //
    //        // Load Project
    //#ifdef _DEBUG
    //        ProjectDaemon::Instance()->OpenProject(ProjectName);
    //#else
    //        ProjectDaemon::Instance()->OpenProject(ProjectName);
    //#endif
    //        // Restart timer after renderer has finished initializing.
    //        m_timer->Reset();
    //
    //        return S_OK;
    //    }

    //--------------------------------------------------------------------------------------

    //void Direct3DBackground::Disconnect()
    //{
    //    //TODO: do a clean disconnect
    //    m_renderer = nullptr;
    //    m_projectRenderer = nullptr;
    //}

    //--------------------------------------------------------------------------------------

    //HRESULT Direct3DBackground::PrepareResources(_In_ const LARGE_INTEGER* presentTargetTime, _Inout_ DrawingSurfaceSizeF* desiredRenderTargetSize)
    //{
    //    m_timer->Update();
    //    m_renderer->Update(m_timer->Total, m_timer->Delta);
    //    m_projectRenderer->Update(m_timer->Total, m_timer->Delta);

    //    // Save this for later
    //    if (!m_initialized)
    //    {
    //        m_originalWindowsBounds.X = desiredRenderTargetSize->width;
    //        m_originalWindowsBounds.Y = desiredRenderTargetSize->height;
    //        ProjectDaemon::Instance()->SetDesiredRenderTargetSize(desiredRenderTargetSize);
    //        m_initialized = true;
    //    }

    //    return S_OK;
    //}

    //--------------------------------------------------------------------------------------

    //HRESULT Direct3DBackground::Draw(_In_ ID3D11Device1* device, _In_ ID3D11DeviceContext1* context, _In_ ID3D11RenderTargetView* renderTargetView)
    //{
    //    //m_context = context;

    //    if (!ProjectDaemon::Instance()->FinishedLoading() || m_renderingErrorOccured)
    //    {
    //        // Render Loading Screen
    //        m_renderer->UpdateDevice(device, context, renderTargetView);
    //        m_renderer->Render();
    //    }
    //    else
    //    {
    //        // Render Project
    //        try
    //        {
    //            m_projectRenderer->UpdateDevice(device, context, renderTargetView);
    //        }
    //        catch (PlayerException *e)
    //        {
    //            m_renderingErrorOccured = true;
    //            ProjectDaemon::Instance()->AddDebug(L"Error Updating Device.");
    //        }
    //        catch (Platform::Exception^ e)
    //        {
    //            m_renderingErrorOccured = true;
    //            ProjectDaemon::Instance()->AddDebug(L"Error Updating Device.");
    //        }

    //        try
    //        {
    //            m_projectRenderer->Render();
    //        }
    //        catch (PlayerException *e)
    //        {
    //            m_renderingErrorOccured = true;
    //            ProjectDaemon::Instance()->AddDebug(L"Rendering not possible.");
    //        }
    //        catch (Platform::Exception^ e)
    //        {
    //            m_renderingErrorOccured = true;
    //            ProjectDaemon::Instance()->AddDebug(L"Rendering not possible.");
    //        }
    //    }

    //    RequestAdditionalFrame();

    //    return S_OK;
    //}

}