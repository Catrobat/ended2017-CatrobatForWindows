#pragma once

#include "pch.h"
#include "BasicTimer.h"
#include "Renderer.h"
#include "SoundManager.h"
#include "ProjectRenderer.h"
#include "WhenScript.h"
#include "EventControllerXaml.h"
//#include <DrawingSurfaceNative.h>
#include "Direct3DBase.h"
#include "DeviceResources.h"

namespace PhoneDirect3DXamlAppComponent
{

public delegate void RequestAdditionalFrameHandler();

//[Windows::Foundation::Metadata::WebHostHidden]
class Direct3DBackground //: public Direct3DBase
{
public:
	Direct3DBackground(Windows::UI::Core::CoreWindow^ coreWindow, Windows::UI::Xaml::Controls::SwapChainPanel^ panel);
	virtual ~Direct3DBackground();
    void StartRenderLoop();
    void StopRenderLoop();
    void Suspend();
    void Resume();
	//virtual void Render() override;

	void SetSwapChainPanel(Windows::UI::Xaml::Controls::SwapChainPanel^ panel);
	Windows::UI::Xaml::Controls::SwapChainPanel^ GetSwapChainPanel() const { return m_swapChainPanel; }

	RequestAdditionalFrameHandler^ RequestAdditionalFrame;

	Windows::Foundation::Size* WindowBounds;
	Windows::Foundation::Size* NativeResolution;
	Windows::Foundation::Size* RenderResolution;
    Platform::String^ ProjectName;

	ID3D11Device1* GetDevice() { return safe_cast<ID3D11Device1*>(m_deviceResources->GetD3DDevice()); }
	ID3D11DeviceContext1* GetContext() { return safe_cast<ID3D11DeviceContext1*>(m_deviceResources->GetD3DDeviceContext()); }
	ID3D11RenderTargetView* GetRenderTargetView() { return m_deviceResources->GetBackBufferRenderTargetView(); }

//internal:
	//HRESULT Connect(_In_ IDrawingSurfaceRuntimeHostNative* host, _In_ ID3D11Device1* device);
	//void Disconnect();

	//HRESULT PrepareResources(_In_ const LARGE_INTEGER* presentTargetTime, _Inout_ DrawingSurfaceSizeF* desiredRenderTargetSize);
	HRESULT Draw(_In_ ID3D11Device1* device, _In_ ID3D11DeviceContext1* context, _In_ ID3D11RenderTargetView* renderTargetView);

private:
	Concurrency::critical_section& GetCriticalSection();

	EventController^								m_eventController;
	Windows::UI::Xaml::Controls::SwapChainPanel^	m_swapChainPanel;

	Renderer^                                       m_renderer;
	ProjectRenderer^                                m_projectRenderer;
	BasicTimer^                                     m_timer;
	Windows::Foundation::Rect                       m_originalWindowsBounds;
	bool                                            m_renderingErrorOccured;
    bool                                            m_initialized;

	std::shared_ptr<DX::DeviceResources>            m_deviceResources;

	// Cached device properties.
	D3D_FEATURE_LEVEL                               m_d3dFeatureLevel;

    Windows::UI::Core::CoreWindow^                  m_coreWindow;
    Concurrency::critical_section                   m_criticalSection;
    Windows::Foundation::IAsyncAction^              m_renderLoopWorker;
};

}