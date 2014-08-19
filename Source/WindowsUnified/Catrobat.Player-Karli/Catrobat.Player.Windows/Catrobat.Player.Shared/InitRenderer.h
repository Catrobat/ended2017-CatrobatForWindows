#pragma once

//#include "Direct3DBaseRenderer.h"
#include "DeviceResources.h"
#include "SpriteBatch.h"
#include "SpriteFont.h"
#include "Project.h"

#include <wrl/client.h>

ref class InitRenderer
{
internal:
    InitRenderer(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    void CreateDeviceIndependentResources();
    void CreateDeviceDependentResources();
    //void CreateWindowSizeDependentResources();
    void ReleaseDeviceDependentResources();
    void Render();

	// Method for updating time-dependent objects.
	//void Update(float timeTotal, float timeDelta);

protected private:
    void CreateInitText();
    void RenderInitText();

    // Cached pointer to device resources.
    std::shared_ptr<DX::DeviceResources>     m_deviceResources;

	bool m_loadingComplete;
	bool m_startup;
	uint32 m_indexCount;

	// SpriteBatch for Drawing (this is needed by all Object Draw Methods -> Use: m_spriteBatch.Get())
	std::unique_ptr<SpriteBatch> m_spriteBatch;
	std::unique_ptr<SpriteFont> m_spriteFont; 

	// Use this scale if you calculate positions on the screen
	float m_scale;
	void StartUpTasks();
    bool m_initialized;

    // Resources related to text rendering.
    std::wstring                                    m_text;
    Microsoft::WRL::ComPtr<IDWriteTextFormat>		m_textFormat;
    Microsoft::WRL::ComPtr<IDWriteTextLayout>       m_textLayout;
    DWRITE_TEXT_METRICS	                            m_textMetrics;
    Microsoft::WRL::ComPtr<ID2D1DrawingStateBlock>  m_stateBlock;
    Microsoft::WRL::ComPtr<ID2D1SolidColorBrush>    m_whiteBrush;
};
