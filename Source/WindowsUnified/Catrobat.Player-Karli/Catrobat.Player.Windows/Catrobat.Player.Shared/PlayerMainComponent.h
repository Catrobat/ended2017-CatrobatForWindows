#pragma once

//#include "pch.h"
#include "Direct3DDeviceResources.h"
#include "InitRenderer.h"
#include "ProjectRenderer.h"

//#include "BasicTimer.h"
//#include "SoundManager.h"
//#include "WhenScript.h"
//#include "EventControllerXaml.h"
//#include "Direct3DBase.h"

//public delegate void RequestAdditionalFrameHandler();

namespace Catrobat_Player
{

    class PlayerMainComponent
    {
    public:
        PlayerMainComponent(const std::shared_ptr<DX::Direct3DDeviceResources>& direct3DDeviceResources,
            Windows::UI::Xaml::Controls::CommandBar^ playerAppBar);
        ~PlayerMainComponent();
        void CreateWindowSizeDependentResources();
        void StartRenderLoop();
        void StopRenderLoop();
        void Suspend();
        void Resume();
        Concurrency::critical_section& GetCriticalSection() { return m_criticalSection; }
        //virtual void Render() override;

        //Windows::UI::Xaml::Controls::SwapChainPanel^ GetSwapChainPanel() const { return m_swapChainPanel; }

        //RequestAdditionalFrameHandler^ RequestAdditionalFrame;

        //Windows::Foundation::Size* WindowBounds;
        //Windows::Foundation::Size* NativeResolution;
        //Windows::Foundation::Size* RenderResolution;
        //   Platform::String^ ProjectName;

        //ID3D11Device1* GetDevice() { return safe_cast<ID3D11Device1*>(m_deviceResources->GetD3DDevice()); }
        //ID3D11DeviceContext1* GetContext() { return safe_cast<ID3D11DeviceContext1*>(m_deviceResources->GetD3DDeviceContext()); }
        //ID3D11RenderTargetView* GetRenderTargetView() { return m_deviceResources->GetBackBufferRenderTargetView(); }

        void WindowActivationChanged(Windows::UI::Core::CoreWindowActivationState activationState);

        //internal:
        //HRESULT Connect(_In_ IDrawingSurfaceRuntimeHostNative* host, _In_ ID3D11Device1* device);
        //void Disconnect();

        //HRESULT PrepareResources(_In_ const LARGE_INTEGER* presentTargetTime, _Inout_ DrawingSurfaceSizeF* desiredRenderTargetSize);
        //HRESULT Draw(_In_ ID3D11Device1* device, _In_ ID3D11DeviceContext1* context, _In_ ID3D11RenderTargetView* renderTargetView);

    private:
        std::shared_ptr<DX::Direct3DDeviceResources>    m_direct3DDeviceResources;
        Windows::UI::Xaml::Controls::CommandBar^        m_playerAppBar;

        InitRenderer^                                   m_initRenderer;
        ProjectRenderer^                                m_projectRenderer;


        //EventController^								m_eventController;
        //BasicTimer^                                     m_timer;
        //Windows::Foundation::Rect                       m_originalWindowsBounds;
        //bool                                            m_renderingErrorOccured;
        //   bool                                            m_initialized;

        Concurrency::critical_section                   m_criticalSection;
        Windows::Foundation::IAsyncAction^              m_renderLoopWorker;
    };

}