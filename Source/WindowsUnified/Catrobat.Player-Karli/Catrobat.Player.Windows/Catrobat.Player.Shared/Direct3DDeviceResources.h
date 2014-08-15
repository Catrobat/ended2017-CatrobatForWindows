#pragma once

namespace DX
{
	// Controls all the DirectX device resources.
	class Direct3DDeviceResources
	{
	public:
        Direct3DDeviceResources();
		void SetSwapChainPanel(Windows::UI::Xaml::Controls::SwapChainPanel^ panel);
		void SetLogicalSize(Windows::Foundation::Size logicalSize);
		//void SetCurrentOrientation(Windows::Graphics::Display::DisplayOrientations currentOrientation);
		void SetDpi(float dpi);
		void SetCompositionScale(float compositionScaleX, float compositionScaleY);
		//void UpdateStereoState();
		//void ValidateDevice();
		void HandleDeviceLost();
		//void RegisterDeviceNotify(IDeviceNotify* deviceNotify);
		//void Trim();
		//void Present();

		// Device Accessors.
		Windows::Foundation::Size GetOutputSize() const                     { return m_outputSize; }
		Windows::Foundation::Size GetLogicalSize() const                    { return m_logicalSize; }
		Windows::Foundation::Size GetRenderTargetSize() const               { return m_d3dRenderTargetSize; }
		Windows::UI::Xaml::Controls::SwapChainPanel^ GetSwapChainPanel() const { return m_playerSwapChainPanel; }

		// D3D Accessors.
		ID3D11Device2*          GetD3DDevice() const                        { return m_d3dDevice.Get(); }
		ID3D11DeviceContext2*   GetD3DDeviceContext() const                 { return m_d3dContext.Get(); }
		IDXGISwapChain1*        GetSwapChain() const                        { return m_swapChain.Get(); }
		D3D_FEATURE_LEVEL       GetDeviceFeatureLevel() const               { return m_d3dFeatureLevel; }
		ID3D11RenderTargetView* GetBackBufferRenderTargetView() const       { return m_d3dRenderTargetView.Get(); }
		ID3D11RenderTargetView* GetBackBufferRenderTargetViewRight() const  { return m_d3dRenderTargetViewRight.Get(); }
		ID3D11DepthStencilView* GetDepthStencilView() const                 { return m_d3dDepthStencilView.Get(); }
		D3D11_VIEWPORT          GetScreenViewport() const                   { return m_screenViewport; }
		//DirectX::XMFLOAT4X4     GetOrientationTransform3D() const           { return m_orientationTransform3D; }

        // bad hack for testing purposes
        Microsoft::WRL::ComPtr<ID3D11DeviceContext2>    m_d3dContext;

        Microsoft::WRL::ComPtr<ID3D11RenderTargetView>  m_d3dRenderTargetView;

        Microsoft::WRL::ComPtr<ID3D11DepthStencilView>  m_d3dDepthStencilView;

        Microsoft::WRL::ComPtr<IDXGISwapChain1>         m_swapChain;

	private:
		void CreateDirect3DDeviceResources();
		void CreateWindowSizeDependentResources();

		// Direct3D objects.
		Microsoft::WRL::ComPtr<ID3D11Device2>           m_d3dDevice;
		
		

		// Direct3D rendering objects. Required for 3D.
		
		Microsoft::WRL::ComPtr<ID3D11RenderTargetView>  m_d3dRenderTargetViewRight;
		
		D3D11_VIEWPORT                                  m_screenViewport;

		// DirectWrite drawing components.
		//Microsoft::WRL::ComPtr<IDWriteFactory2>         m_dwriteFactory;
		//Microsoft::WRL::ComPtr<IWICImagingFactory2>     m_wicFactory;

		// Cached reference to the XAML panel.
		Windows::UI::Xaml::Controls::SwapChainPanel^      m_playerSwapChainPanel;

		// Cached device properties.
		D3D_FEATURE_LEVEL                                 m_d3dFeatureLevel;
		Windows::Foundation::Size                         m_d3dRenderTargetSize;
		Windows::Foundation::Size                         m_outputSize;
		Windows::Foundation::Size                         m_logicalSize;
		Windows::Graphics::Display::DisplayOrientations   m_nativeOrientation;
		Windows::Graphics::Display::DisplayOrientations   m_currentOrientation;
		float                                             m_dpi;
		float                                             m_compositionScaleX;
		float                                             m_compositionScaleY;
	};
}