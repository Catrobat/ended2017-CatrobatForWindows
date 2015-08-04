 #pragma once

#include <d2d1.h>
#include "Common\DeviceResources.h"
#include "Common\StepTimer.h"

class Basic2DRenderer
{
public:
    Basic2DRenderer(const std::shared_ptr<DX::DeviceResources>& deviceResources);
	void Initialize();
    void CreateDeviceDependentResources();
    void CreateWindowSizeDependentResources();
    void ReleaseDeviceDependentResources();
    void Update(DX::StepTimer const& timer);
    void Render();
    void PointerPressed(D2D1_POINT_2F point);

private:
    // Cached pointer to device resources.
    std::shared_ptr<DX::DeviceResources> m_deviceResources;

    bool m_tracking;
};

