#include "pch.h"
#include "PlayerWindowsPhone8Component.h"
#include "Direct3DContentProvider.h"
#include "XMLParser.h"
#include "ProjectDaemon.h"
#include "ScriptHandler.h"
#include "WhenScript.h"
#include "lodepng.h"
#include "lodepng_util.h"
#include "DDSLoader.h"
#include "Interpreter.h"
#include "XMLParserFatalException.h"
#include "PlayerException.h"

#include <windows.system.threading.h>
#include <exception>
#include <windows.foundation.h>
#include <thread>
#include <math.h>

using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Microsoft::WRL;
using namespace Windows::Phone::Graphics::Interop;
using namespace Windows::Phone::Input::Interop;
using namespace Windows::Graphics::Display;
using namespace Windows::System::Threading;
using namespace Windows::Phone::UI::Input;

namespace PhoneDirect3DXamlAppComponent
{
    //--------------------------------------------------------------------------------------

    Direct3DBackground::Direct3DBackground(Windows::UI::Core::CoreWindow^ coreWindow) :
        m_coreWindow(coreWindow),
        m_timer(ref new BasicTimer())
    {
        ProjectDaemon::Instance()->ReInit();
        m_initialized = false;

        InitEventHandlers();
        m_renderLoopWorker = nullptr;
    }

    //--------------------------------------------------------------------------------------

    Direct3DBackground::~Direct3DBackground()
    {
        m_initialized = false;
    }

    //--------------------------------------------------------------------------------------

    Direct3DBackground::InitEventHandlers()
    {
        m_coreWindow->PointerPressed +=
            ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &Direct3DBackground::OnPointerPressed);

        m_coreWindow->PointerMoved +=
            ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &Direct3DBackground::OnPointerMoved);

        m_coreWindow->PointerReleased +=
            ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &Direct3DBackground::OnPointerReleased);

        m_coreWindow->PointerExited +=
            ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &Direct3DBackground::OnPointerExited);

        HardwareButtons::BackPressed +=
            ref new EventHandler<BackPressedEventArgs^>(this, &Direct3DBackground::OnHardwareBackButtonPressed);


    }

    //--------------------------------------------------------------------------------------

    void Direct3DBackground::StartRenderLoop()
    {
        // TODO: FIXXME

        // If the animation render loop is already running then do not start another thread.
        if (m_renderLoopWorker != nullptr && m_renderLoopWorker->Status == Windows::Foundation::AsyncStatus::Started)
        {
            return;
        }

        // Create a task that will be run on a background thread.
        auto workItemHandler = ref new WorkItemHandler([this](IAsyncAction ^ action)
        {
            // Calculate the updated frame and render once per vertical blanking interval.
            while (action->Status == Windows::Foundation::AsyncStatus::Started)
            {
                //critical_section::scoped_lock lock(m_criticalSection);
                //Update();
                //m_renderer->Render();
                //m_deviceResources->Present();

                //if (!m_haveFocus || (m_updateState == UpdateEngineState::TooSmall))
                //{
                //    // The app is in an inactive state so stop rendering
                //    // This optimizes for power and allows the framework to become more queiecent
                //    break;
                //}
            }
        });

        // Run task on a dedicated high priority background thread.
        m_renderLoopWorker = ThreadPool::RunAsync(workItemHandler, WorkItemPriority::High, WorkItemOptions::TimeSliced);
    }

    //--------------------------------------------------------------------------------------

    void Direct3DBackground::StopRenderLoop()
    {
        m_renderLoopWorker->Cancel();
    }

    //--------------------------------------------------------------------------------------

    void Direct3DBackground::Suspend()
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

    void Direct3DBackground::Resume()
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

    // Event Handlers
    //--------------------------------------------------------------------------------------

    void Direct3DBackground::OnPointerPressed(
        _In_ CoreWindow^ sender,
        _In_ PointerEventArgs^ args
        )
    {
        if (!ProjectDaemon::Instance()->FinishedLoading())
        {
            return;
        }

        Project* project = ProjectDaemon::Instance()->GetProject();
        ObjectList* objects = project->GetObjectList();

        if (objects == NULL)
        {
            return;
        }

        bool objectFound = false;

        for (int i = objects->GetSize() - 1; i >= 0; i--)
        {
            try
            {
                float resolutionScaleFactor;

                switch (DisplayProperties::ResolutionScale)
                {
                case ResolutionScale::Scale100Percent:
                    resolutionScaleFactor = 1.0f;
                    break;
                case ResolutionScale::Scale150Percent:
                    resolutionScaleFactor = 1.5f;
                    break;
                case ResolutionScale::Scale160Percent:
                    resolutionScaleFactor = 1.6f;
                    break;
                }

                double actualX = args->CurrentPoint->Position.X;
                double actualY = args->CurrentPoint->Position.Y;

                double factorX = abs(ProjectDaemon::Instance()->GetProject()->GetScreenWidth() / (m_originalWindowsBounds.X / resolutionScaleFactor));
                double factorY = abs(ProjectDaemon::Instance()->GetProject()->GetScreenHeight() / (m_originalWindowsBounds.Y / resolutionScaleFactor));

                double normalizedX = actualX * ((int)DisplayProperties::ResolutionScale) / 100.0;
                double normalizedY = actualY * ((int)DisplayProperties::ResolutionScale) / 100.0;

                auto current = objects->GetObject(i);

                if (current->IsTapPointHitting(m_context, m_device, actualX, actualY, (double)((int)DisplayProperties::ResolutionScale) / 100.0))
                {
                    for (int j = 0; j < current->GetScriptListSize(); j++)
                    {
                        Script *script = current->GetScript(j);

                        if (script->GetType() == Script::TypeOfScript::WhenScript)
                        {
                            if (current->GetWhenScript() != NULL && current->GetWhenScript()->IsRunning())
                            {
                                return;
                            }
                            else
                            {
                                WhenScript *whenScript = (WhenScript *)script;

                                if (whenScript->GetAction() == WhenScript::Action::Tapped)
                                {
                                    current->SetWhenScript(whenScript);
                                    whenScript->Execute();
                                    objectFound = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (objectFound)
                    break;
            }
            catch (PlayerException *e) //TODO: Exception handling
            {
            }
            catch (Platform::Exception ^e) //TODO: Exception handling
            {
            }
        }
    }

    //--------------------------------------------------------------------------------------

    void Direct3DBackground::OnPointerMoved(
        _In_ CoreWindow^ sender,
        _In_ PointerEventArgs^ args
        ) 
    { 
        // TODO: implement me
    }
    
    //--------------------------------------------------------------------------------------

    void Direct3DBackground::OnPointerReleased(
        _In_ CoreWindow^ sender,
        _In_ PointerEventArgs^ args
        ) 
    { 
        // TODO: implement me
    }

    //--------------------------------------------------------------------------------------

    void Direct3DBackground::OnPointerExited(
        _In_ CoreWindow^ sender,
        _In_ PointerEventArgs^ args
        )
    {
       // TODO: implement me
    }

    //----------------------------------------------------------------------

    void Direct3DBackground::OnHardwareBackButtonPressed(
        _In_ Platform::Object^ sender,
        BackPressedEventArgs ^args
        )
    {
        // TODO: implement me

        if (/*m_state == MoveLookControllerState::Active*/)
        {
            // The game is currently in active play mode, so hitting the hardware back button
            // will cause the game to pause.
            //m_pausePressed = true;
            args->Handled = false;
        }
        else
        {
            // The game is not currently in active play mode, so take the default behavior
            // for the hardware back button.
            args->Handled = false;
        }
    }

    //--------------------------------------------------------------------------------------

    HRESULT Direct3DBackground::Connect(_In_ IDrawingSurfaceRuntimeHostNative* host, _In_ ID3D11Device1* device)
    {
        // Initialize Renderer
        m_renderer = ref new Renderer();
        m_renderer->Initialize(device);
        m_device = device;

        // Initialize Sound
        SoundManager::Instance()->Initialize();

        //Initialize Project Renderer
        m_renderingErrorOccured = false;
        m_projectRenderer = ref new ProjectRenderer();
        ProjectDaemon::Instance()->SetupRenderer(device, m_projectRenderer);

        // Load Project
#ifdef _DEBUG
        ProjectDaemon::Instance()->OpenProject(ProjectName);
#else
        ProjectDaemon::Instance()->OpenProject(ProjectName);
#endif
        // Restart timer after renderer has finished initializing.
        m_timer->Reset();

        return S_OK;
    }

    //--------------------------------------------------------------------------------------

    void Direct3DBackground::Disconnect()
    {
        //TODO: do a clean disconnect
        m_renderer = nullptr;
        m_projectRenderer = nullptr;
    }

    //--------------------------------------------------------------------------------------

    HRESULT Direct3DBackground::PrepareResources(_In_ const LARGE_INTEGER* presentTargetTime, _Inout_ DrawingSurfaceSizeF* desiredRenderTargetSize)
    {
        m_timer->Update();
        m_renderer->Update(m_timer->Total, m_timer->Delta);
        m_projectRenderer->Update(m_timer->Total, m_timer->Delta);

        // Save this for later
        if (!m_initialized)
        {
            m_originalWindowsBounds.X = desiredRenderTargetSize->width;
            m_originalWindowsBounds.Y = desiredRenderTargetSize->height;
            ProjectDaemon::Instance()->SetDesiredRenderTargetSize(desiredRenderTargetSize);
            m_initialized = true;
        }

        return S_OK;
    }

    //--------------------------------------------------------------------------------------

    HRESULT Direct3DBackground::Draw(_In_ ID3D11Device1* device, _In_ ID3D11DeviceContext1* context, _In_ ID3D11RenderTargetView* renderTargetView)
    {
        m_context = context;

        if (!ProjectDaemon::Instance()->FinishedLoading() || m_renderingErrorOccured)
        {
            // Render Loading Screen
            m_renderer->UpdateDevice(device, context, renderTargetView);
            m_renderer->Render();
        }
        else
        {
            // Render Project
            try
            {
                m_projectRenderer->UpdateDevice(device, context, renderTargetView);
            }
            catch (PlayerException *e)
            {
                m_renderingErrorOccured = true;
                ProjectDaemon::Instance()->AddDebug(L"Error Updating Device.");
            }
            catch (Platform::Exception^ e)
            {
                m_renderingErrorOccured = true;
                ProjectDaemon::Instance()->AddDebug(L"Error Updating Device.");
            }

            try
            {
                m_projectRenderer->Render();
            }
            catch (PlayerException *e)
            {
                m_renderingErrorOccured = true;
                ProjectDaemon::Instance()->AddDebug(L"Rendering not possible.");
            }
            catch (Platform::Exception^ e)
            {
                m_renderingErrorOccured = true;
                ProjectDaemon::Instance()->AddDebug(L"Rendering not possible.");
            }
        }

        RequestAdditionalFrame();

        return S_OK;
    }

}