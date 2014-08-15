#pragma once

//#include "Direct3DBaseRenderer.h"
#include "Direct3DDeviceResources.h"
#include "SpriteBatch.h"
#include "SpriteFont.h"
#include "Project.h"

#include <wrl/client.h>

ref class InitRenderer
{
internal:
    InitRenderer(const std::shared_ptr<DX::Direct3DDeviceResources>& direct3DDeviceResources);

	// Direct3DBase methods.
    //void CreateDeviceDependentResources();
    //void CreateWindowSizeDependentResources();
    //void ReleaseDeviceDependentResources();
    void Render();

	// Method for updating time-dependent objects.
	//void Update(float timeTotal, float timeDelta);

protected private:
    // Cached pointer to device resources.
    std::shared_ptr<DX::Direct3DDeviceResources>     m_direct3DDeviceResources;

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
};
