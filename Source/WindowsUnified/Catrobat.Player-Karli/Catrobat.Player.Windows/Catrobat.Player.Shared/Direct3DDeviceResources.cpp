#include "pch.h"
#include "Direct3DDeviceResources.h"
#include "DirectXHelper.h"
#include <windows.ui.xaml.media.dxinterop.h>

//using namespace DirectX;
using namespace Microsoft::WRL;
//using namespace Windows::Foundation;
using namespace Windows::Graphics::Display;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml::Controls;
using namespace Platform;

namespace DX 
{

    // Constructor for DeviceResources.
    Direct3DDeviceResources::Direct3DDeviceResources():
        m_screenViewport(),
        m_d3dFeatureLevel(D3D_FEATURE_LEVEL_9_1),
        m_d3dRenderTargetSize(),
        m_outputSize(),
        m_logicalSize(),
        m_nativeOrientation(DisplayOrientations::None),
        m_currentOrientation(DisplayOrientations::None),
        m_dpi(-1.0f),
        m_compositionScaleX(1.0f),
        m_compositionScaleY(1.0f)
        //m_deviceNotify(nullptr)
    {
        CreateDirect3DDeviceResources();
    }

    // Configures the Direct3D device, and stores handles to it and the device context.
    void Direct3DDeviceResources::CreateDirect3DDeviceResources()
    {
        // This flag adds support for surfaces with a different color channel ordering
        // than the API default. It is required for compatibility with Direct2D.
        UINT creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;

#if defined(_DEBUG)
        if (DX::SdkLayersAvailable())
        {
            // If the project is in a debug build, enable debugging via SDK Layers with this flag.
            creationFlags |= D3D11_CREATE_DEVICE_DEBUG;
        }
#endif

        // This array defines the set of DirectX hardware feature levels this app will support.
        // Note the ordering should be preserved.
        // Don't forget to declare your application's minimum required feature level in its
        // description.  All applications are assumed to support 9.1 unless otherwise stated.
        D3D_FEATURE_LEVEL featureLevels[] =
        {
            D3D_FEATURE_LEVEL_11_1,
            D3D_FEATURE_LEVEL_11_0,
            D3D_FEATURE_LEVEL_10_1,
            D3D_FEATURE_LEVEL_10_0,
            D3D_FEATURE_LEVEL_9_3,
            D3D_FEATURE_LEVEL_9_2,
            D3D_FEATURE_LEVEL_9_1
        };

        // Create the Direct3D 11 API device object and a corresponding context.
        ComPtr<ID3D11Device> device;
        ComPtr<ID3D11DeviceContext> context;

        HRESULT hr = D3D11CreateDevice(
            nullptr,                    // Specify nullptr to use the default adapter.
            D3D_DRIVER_TYPE_HARDWARE,   // Create a device using the hardware graphics driver.
            0,                          // Should be 0 unless the driver is D3D_DRIVER_TYPE_SOFTWARE.
            creationFlags,              // Set debug and Direct2D compatibility flags.
            featureLevels,              // List of feature levels this app can support.
            ARRAYSIZE(featureLevels),   // Size of the list above.
            D3D11_SDK_VERSION,          // Always set this to D3D11_SDK_VERSION for Windows Store apps.
            &device,                    // Returns the Direct3D device created.
            &m_d3dFeatureLevel,         // Returns feature level of device created.
            &context                    // Returns the device immediate context.
            );

        if (FAILED(hr))
        {
            // If the initialization fails, fall back to the WARP device.
            // For more information on WARP, see: 
            // http://go.microsoft.com/fwlink/?LinkId=286690
            DX::ThrowIfFailed(
                D3D11CreateDevice(
                nullptr,
                D3D_DRIVER_TYPE_WARP, // Create a WARP device instead of a hardware device.
                0,
                creationFlags,
                featureLevels,
                ARRAYSIZE(featureLevels),
                D3D11_SDK_VERSION,
                &device,
                &m_d3dFeatureLevel,
                &context
                )
                );
        }

        // Store pointers to the Direct3D 11.1 API device and immediate context.
        DX::ThrowIfFailed(
            device.As(&m_d3dDevice)
            );

        DX::ThrowIfFailed(
            context.As(&m_d3dContext)
            );
    }

    // These resources need to be recreated every time the window size is changed.
    void Direct3DDeviceResources::CreateWindowSizeDependentResources()
    {
        OutputDebugString(L"Direct3DDeviceResources::CreateWindowSizeDependentResources\n");
        // Clear the previous window size specific context.
        ID3D11RenderTargetView* nullViews[] = { nullptr };
        m_d3dContext->OMSetRenderTargets(ARRAYSIZE(nullViews), nullViews, nullptr);
        m_d3dRenderTargetView = nullptr;
        m_d3dRenderTargetViewRight = nullptr;
        m_d3dDepthStencilView = nullptr;
        m_d3dContext->Flush();

        // Calculate the necessary swap chain and render target size in pixels.
        m_outputSize.Width = m_logicalSize.Width * m_compositionScaleX;
        m_outputSize.Height = m_logicalSize.Height * m_compositionScaleY;

        // Prevent zero size DirectX content from being created.
        m_outputSize.Width = max(m_outputSize.Width, 1);
        m_outputSize.Height = max(m_outputSize.Height, 1);

#if WINAPI_FAMILY == WINAPI_FAMILY_PHONE_APP
        // The WindowsPhone version of SwapChainPanel always automatically rotates the
        // the swapchain to the screen orientation.  For Windows Phone we disable
        // the optimization of doing drawing in the native orientation to avoid a
        // rotate copy.
        auto displayRotation = DXGI_MODE_ROTATION_IDENTITY;
        m_d3dRenderTargetSize.Width = m_outputSize.Width;
        m_d3dRenderTargetSize.Height = m_outputSize.Height;
#else
        // The width and height of the swap chain must be based on the window's
        // natively-oriented width and height. If the window is not in the native
        // orientation, the dimensions must be reversed.
        // We do this to take advantage on an optimization that allows for the 
        // application to draw the content in the proper orientation and then
        // indicate to DXGI that the content has been pre-rotated.

        // NOTE: activate the following code, when targeting also the store app

        //DXGI_MODE_ROTATION displayRotation = ComputeDisplayRotation();

        //bool swapDimensions = displayRotation == DXGI_MODE_ROTATION_ROTATE90 || displayRotation == DXGI_MODE_ROTATION_ROTATE270;
        //m_d3dRenderTargetSize.Width = swapDimensions ? m_outputSize.Height : m_outputSize.Width;
        //m_d3dRenderTargetSize.Height = swapDimensions ? m_outputSize.Width : m_outputSize.Height;
#endif

        if (m_swapChain != nullptr)
        {
            // If the swap chain already exists, resize it.
            HRESULT hr = m_swapChain->ResizeBuffers(
                2, // Double-buffered swap chain.
                static_cast<UINT>(m_d3dRenderTargetSize.Width),
                static_cast<UINT>(m_d3dRenderTargetSize.Height),
                DXGI_FORMAT_B8G8R8A8_UNORM,
                0
                );

            if (hr == DXGI_ERROR_DEVICE_REMOVED || hr == DXGI_ERROR_DEVICE_RESET)
            {
                // If the device was removed for any reason, a new device and swap chain will need to be created.
                HandleDeviceLost();

                // Everything is set up now. Do not continue execution of this method. HandleDeviceLost will reenter this method 
                // and correctly set up the new device.
                return;
            }
            else
            {
                DX::ThrowIfFailed(hr);
            }
        }
        else
        {
            // Otherwise, create a new one using the same adapter as the existing Direct3D device.
            DXGI_SWAP_CHAIN_DESC1 swapChainDesc = { 0 };

            swapChainDesc.Width = static_cast<UINT>(m_d3dRenderTargetSize.Width); // Match the size of the window.
            swapChainDesc.Height = static_cast<UINT>(m_d3dRenderTargetSize.Height);
            swapChainDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM; // This is the most common swap chain format.
            swapChainDesc.Stereo = false; // m_stereoEnabled; NOTE --> not supported yet
            swapChainDesc.SampleDesc.Count = 1; // Don't use multi-sampling.
            swapChainDesc.SampleDesc.Quality = 0;
            swapChainDesc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
            swapChainDesc.BufferCount = 2; // Use double-buffering to minimize latency.
            swapChainDesc.SwapEffect = DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL; // All Windows Store apps must use this SwapEffect.
            swapChainDesc.Flags = 0;
            swapChainDesc.Scaling = DXGI_SCALING_STRETCH;
            swapChainDesc.AlphaMode = DXGI_ALPHA_MODE_IGNORE;

            // This sequence obtains the DXGI factory that was used to create the Direct3D device above.
            ComPtr<IDXGIDevice3> dxgiDevice;
            DX::ThrowIfFailed(
                m_d3dDevice.As(&dxgiDevice)
                );

            ComPtr<IDXGIAdapter> dxgiAdapter;
            DX::ThrowIfFailed(
                dxgiDevice->GetAdapter(&dxgiAdapter)
                );

            ComPtr<IDXGIFactory2> dxgiFactory;
            DX::ThrowIfFailed(
                dxgiAdapter->GetParent(IID_PPV_ARGS(&dxgiFactory))
                );

            // When using XAML interop, the swap chain must be created for composition.
            DX::ThrowIfFailed(
                dxgiFactory->CreateSwapChainForComposition(
                m_d3dDevice.Get(),
                &swapChainDesc,
                nullptr,
                &m_swapChain
                )
                );

            // Associate swap chain with SwapChainPanel
            // UI changes will need to be dispatched back to the UI thread
            m_playerSwapChainPanel->Dispatcher->RunAsync(CoreDispatcherPriority::High, ref new DispatchedHandler([=]()
            {
                // Get backing native interface for SwapChainPanel
                ComPtr<ISwapChainPanelNative> panelNative;
                DX::ThrowIfFailed(
                    reinterpret_cast<IUnknown*>(m_playerSwapChainPanel)->QueryInterface(IID_PPV_ARGS(&panelNative))
                    );

                DX::ThrowIfFailed(
                    panelNative->SetSwapChain(m_swapChain.Get())
                    );
            }, CallbackContext::Any));

            // Ensure that DXGI does not queue more than one frame at a time. This both reduces latency and
            // ensures that the application will only render after each VSync, minimizing power consumption.
            DX::ThrowIfFailed(
                dxgiDevice->SetMaximumFrameLatency(1)
                );
        }

        // Set the proper orientation for the swap chain, and generate 2D and
        // 3D matrix transformations for rendering to the rotated swap chain.
        // Note the rotation angle for the 2D and 3D transforms are different.
        // This is due to the difference in coordinate spaces.  Additionally,
        // the 3D matrix is specified explicitly to avoid rounding errors.

        // NOTE: enable this code, when landscape mode is supported
        /*switch (displayRotation)
        {
        case DXGI_MODE_ROTATION_IDENTITY:
            m_orientationTransform2D = Matrix3x2F::Identity();
            m_orientationTransform3D = ScreenRotation::Rotation0;
            break;

        case DXGI_MODE_ROTATION_ROTATE90:
            m_orientationTransform2D =
                Matrix3x2F::Rotation(90.0f) *
                Matrix3x2F::Translation(m_logicalSize.Height, 0.0f);
            m_orientationTransform3D = ScreenRotation::Rotation270;
            break;

        case DXGI_MODE_ROTATION_ROTATE180:
            m_orientationTransform2D =
                Matrix3x2F::Rotation(180.0f) *
                Matrix3x2F::Translation(m_logicalSize.Width, m_logicalSize.Height);
            m_orientationTransform3D = ScreenRotation::Rotation180;
            break;

        case DXGI_MODE_ROTATION_ROTATE270:
            m_orientationTransform2D =
                Matrix3x2F::Rotation(270.0f) *
                Matrix3x2F::Translation(0.0f, m_logicalSize.Width);
            m_orientationTransform3D = ScreenRotation::Rotation90;
            break;

        default:
            throw ref new FailureException();
        }*/

        DX::ThrowIfFailed(
            m_swapChain->SetRotation(displayRotation)
            );

        // Setup inverse scale on the swap chain
        DXGI_MATRIX_3X2_F inverseScale = { 0 };
        inverseScale._11 = 1.0f / m_compositionScaleX;
        inverseScale._22 = 1.0f / m_compositionScaleY;
        ComPtr<IDXGISwapChain2> spSwapChain2;
        DX::ThrowIfFailed(
            m_swapChain.As<IDXGISwapChain2>(&spSwapChain2)
            );

        DX::ThrowIfFailed(
            spSwapChain2->SetMatrixTransform(&inverseScale)
            );

        // Create a render target view of the swap chain back buffer.
        ComPtr<ID3D11Texture2D> backBuffer;
        DX::ThrowIfFailed(
            m_swapChain->GetBuffer(0, IID_PPV_ARGS(&backBuffer))
            );

        DX::ThrowIfFailed(
            m_d3dDevice->CreateRenderTargetView(
            backBuffer.Get(),
            nullptr,
            &m_d3dRenderTargetView
            )
            );

        // Create a depth stencil view for use with 3D rendering if needed.
        CD3D11_TEXTURE2D_DESC depthStencilDesc(
            DXGI_FORMAT_D24_UNORM_S8_UINT,
            static_cast<UINT>(m_d3dRenderTargetSize.Width),
            static_cast<UINT>(m_d3dRenderTargetSize.Height),
            1, // This depth stencil view has only one texture.
            1, // Use a single mipmap level.
            D3D11_BIND_DEPTH_STENCIL
            );

        ComPtr<ID3D11Texture2D> depthStencil;
        DX::ThrowIfFailed(
            m_d3dDevice->CreateTexture2D(
            &depthStencilDesc,
            nullptr,
            &depthStencil
            )
            );

        CD3D11_DEPTH_STENCIL_VIEW_DESC depthStencilViewDesc(D3D11_DSV_DIMENSION_TEXTURE2D);
        DX::ThrowIfFailed(
            m_d3dDevice->CreateDepthStencilView(
            depthStencil.Get(),
            &depthStencilViewDesc,
            &m_d3dDepthStencilView
            )
            );

        // Set the 3D rendering viewport to target the entire window.
        m_screenViewport = CD3D11_VIEWPORT(
            0.0f,
            0.0f,
            m_d3dRenderTargetSize.Width,
            m_d3dRenderTargetSize.Height
            );

        m_d3dContext->RSSetViewports(1, &m_screenViewport);
    }

    // This method is called when the XAML control is created (or re-created).
    void Direct3DDeviceResources::SetSwapChainPanel(SwapChainPanel^ panel)
    {
        OutputDebugString(L"Direct3DDeviceResources::SetSwapChainPanel\n");
        DisplayInformation^ currentDisplayInformation = DisplayInformation::GetForCurrentView();

        m_playerSwapChainPanel = panel;
        m_logicalSize = Windows::Foundation::Size(static_cast<float>(panel->ActualWidth), static_cast<float>(panel->ActualHeight));
        m_nativeOrientation = currentDisplayInformation->NativeOrientation;
        m_currentOrientation = currentDisplayInformation->CurrentOrientation;
        m_compositionScaleX = panel->CompositionScaleX;
        m_compositionScaleY = panel->CompositionScaleY;
        m_dpi = currentDisplayInformation->LogicalDpi;

        CreateWindowSizeDependentResources();
    }

    // This method is called in the event handler for the SizeChanged event.
    void Direct3DDeviceResources::SetLogicalSize(Windows::Foundation::Size logicalSize)
    {
        OutputDebugString(L"Direct3DDeviceResources::SetLogicalSize\n");
        if (m_logicalSize != logicalSize)
        {
            m_logicalSize = logicalSize;
            CreateWindowSizeDependentResources();
        }
    }

    // This method is called in the event handler for the DpiChanged event.
    void Direct3DDeviceResources::SetDpi(float dpi)
    {
        OutputDebugString(L"Direct3DDeviceResources::SetDpi\n");
        if (dpi != m_dpi)
        {
            m_dpi = dpi;
            //m_d2dContext->SetDpi(m_dpi, m_dpi);
            CreateWindowSizeDependentResources();
        }
    }

    // This method is called in the event handler for the OrientationChanged event.
    //void DX::DeviceResources::SetCurrentOrientation(DisplayOrientations currentOrientation)
    //{
    //    if (m_currentOrientation != currentOrientation)
    //    {
    //        m_currentOrientation = currentOrientation;
    //        CreateWindowSizeDependentResources();
    //    }
    //}

    // This method is called in the event handler for the CompositionScaleChanged event.
    void Direct3DDeviceResources::SetCompositionScale(float compositionScaleX, float compositionScaleY)
    {
        OutputDebugString(L"Direct3DDeviceResources::SetCompositionScale\n");
        if (m_compositionScaleX != compositionScaleX ||
            m_compositionScaleY != compositionScaleY)
        {
            m_compositionScaleX = compositionScaleX;
            m_compositionScaleY = compositionScaleY;
            CreateWindowSizeDependentResources();
        }
    }

    // This method is called in the event handler for the DisplayContentsInvalidated event.
    //void DX::DeviceResources::ValidateDevice()
    //{
    //    // The D3D Device is no longer valid if the default adapter changed since the device
    //    // was created or if the device has been removed.

    //    // First, get the information for the default adapter from when the device was created.

    //    ComPtr<IDXGIDevice3> dxgiDevice;
    //    DX::ThrowIfFailed(m_d3dDevice.As(&dxgiDevice));

    //    ComPtr<IDXGIAdapter> deviceAdapter;
    //    DX::ThrowIfFailed(dxgiDevice->GetAdapter(&deviceAdapter));

    //    ComPtr<IDXGIFactory2> deviceFactory;
    //    DX::ThrowIfFailed(deviceAdapter->GetParent(IID_PPV_ARGS(&deviceFactory)));

    //    ComPtr<IDXGIAdapter1> previousDefaultAdapter;
    //    DX::ThrowIfFailed(deviceFactory->EnumAdapters1(0, &previousDefaultAdapter));

    //    DXGI_ADAPTER_DESC previousDesc;
    //    DX::ThrowIfFailed(previousDefaultAdapter->GetDesc(&previousDesc));

    //    // Next, get the information for the current default adapter.

    //    ComPtr<IDXGIFactory2> currentFactory;
    //    DX::ThrowIfFailed(CreateDXGIFactory1(IID_PPV_ARGS(&currentFactory)));

    //    ComPtr<IDXGIAdapter1> currentDefaultAdapter;
    //    DX::ThrowIfFailed(currentFactory->EnumAdapters1(0, &currentDefaultAdapter));

    //    DXGI_ADAPTER_DESC currentDesc;
    //    DX::ThrowIfFailed(currentDefaultAdapter->GetDesc(&currentDesc));

    //    // If the adapter LUIDs don't match, or if the device reports that it has been removed,
    //    // a new D3D device must be created.

    //    if (previousDesc.AdapterLuid.LowPart != currentDesc.AdapterLuid.LowPart ||
    //        previousDesc.AdapterLuid.HighPart != currentDesc.AdapterLuid.HighPart ||
    //        FAILED(m_d3dDevice->GetDeviceRemovedReason()))
    //    {
    //        // Release references to resources related to the old device.
    //        dxgiDevice = nullptr;
    //        deviceAdapter = nullptr;
    //        deviceFactory = nullptr;
    //        previousDefaultAdapter = nullptr;

    //        // Create a new device and swap chain.
    //        HandleDeviceLost();
    //    }
    //}

    // Recreate all device resources and set them back to the current state.
    void Direct3DDeviceResources::HandleDeviceLost()
    {
        OutputDebugString(L"Direct3DDeviceResources::HandleDeviceLost --> implement me!\n");
        // TODO: implement me

        //m_swapChain = nullptr;

        //if (m_deviceNotify != nullptr)
        //{
        //    m_deviceNotify->OnDeviceLost();
        //}

        //// Make sure the rendering state has been released.
        //m_d3dContext->OMSetRenderTargets(0, nullptr, nullptr);
        //m_d3dDepthStencilView = nullptr;
        //m_d3dRenderTargetView = nullptr;
        //m_d3dRenderTargetViewRight = nullptr;

        //m_d3dContext->Flush();

        //CreateWindowSizeDependentResources();

        //if (m_deviceNotify != nullptr)
        //{
        //    m_deviceNotify->OnDeviceRestored();
        //}
    }

    // Call this method when the app suspends. It provides a hint to the driver that the app 
    // is entering an idle state and that temporary buffers can be reclaimed for use by other apps.
    //void DX::DeviceResources::Trim()
    //{
    //    ComPtr<IDXGIDevice3> dxgiDevice;
    //    m_d3dDevice.As(&dxgiDevice);

    //    dxgiDevice->Trim();
    //}

    // Present the contents of the swap chain to the screen.
    //void DX::DeviceResources::Present()
    //{
    //    // The first argument instructs DXGI to block until VSync, putting the application
    //    // to sleep until the next VSync. This ensures we don't waste any cycles rendering
    //    // frames that will never be displayed to the screen.
    //    HRESULT hr = m_swapChain->Present(1, 0);

    //    // Discard the contents of the render target.
    //    // This is a valid operation only when the existing contents will be entirely
    //    // overwritten. If dirty or scroll rects are used, this call should be removed.
    //    m_d3dContext->DiscardView(m_d3dRenderTargetView.Get());

    //    // Discard the contents of the depth stencil.
    //    m_d3dContext->DiscardView(m_d3dDepthStencilView.Get());

    //    // If the device was removed either by a disconnection or a driver upgrade, we 
    //    // must recreate all device resources.
    //    if (hr == DXGI_ERROR_DEVICE_REMOVED || hr == DXGI_ERROR_DEVICE_RESET)
    //    {
    //        HandleDeviceLost();
    //    }
    //    else
    //    {
    //        DX::ThrowIfFailed(hr);
    //    }
    //}



}