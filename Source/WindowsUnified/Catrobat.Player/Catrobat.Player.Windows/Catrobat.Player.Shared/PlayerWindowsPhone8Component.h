#pragma once

#include "pch.h"
#include "BasicTimer.h"
#include "Renderer.h"
#include "SoundManager.h"
#include "ProjectRenderer.h"
#include "WhenScript.h"
#include <DrawingSurfaceNative.h>

namespace PhoneDirect3DXamlAppComponent
{

public delegate void RequestAdditionalFrameHandler();

[Windows::Foundation::Metadata::WebHostHidden]
public ref class Direct3DBackground sealed
{
public:
    Direct3DBackground(Windows::UI::Core::CoreWindow^ coreWindow);
	virtual ~Direct3DBackground();

	event RequestAdditionalFrameHandler^ RequestAdditionalFrame;

	property Windows::Foundation::Size WindowBounds;
	property Windows::Foundation::Size NativeResolution;
	property Windows::Foundation::Size RenderResolution;
    property Platform::String^ ProjectName;

protected:
	// Event Handlers
    void InitEventHandlers();

    void OnPointerPressed(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::PointerEventArgs^ args
        );
    void OnPointerMoved(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::PointerEventArgs^ args
        );
    void OnPointerReleased(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::PointerEventArgs^ args
        );
    void OnPointerExited(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::PointerEventArgs^ args
        );

internal:
	HRESULT Connect(_In_ IDrawingSurfaceRuntimeHostNative* host, _In_ ID3D11Device1* device);
	void Disconnect();

	HRESULT PrepareResources(_In_ const LARGE_INTEGER* presentTargetTime, _Inout_ DrawingSurfaceSizeF* desiredRenderTargetSize);
	HRESULT Draw(_In_ ID3D11Device1* device, _In_ ID3D11DeviceContext1* context, _In_ ID3D11RenderTargetView* renderTargetView);

private:
	Renderer^ m_renderer;
	ProjectRenderer^ m_projectRenderer;
	BasicTimer^ m_timer;
	Windows::Foundation::Rect m_originalWindowsBounds;
	bool m_renderingErrorOccured;
    bool m_initialized;
    ID3D11DeviceContext1* m_context;
    ID3D11Device1* m_device;

    Windows::UI::Core::CoreWindow^ m_coreWindow;
};

}