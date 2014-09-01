#pragma once

#include <d2d1.h>

class Basic2DRenderer
{
public:
    Basic2DRenderer(const std::shared_ptr<DX::DeviceResources>& deviceResources);
    void CreateDeviceDependentResources();
    void ReleaseDeviceDependentResources();
    void Update(DX::StepTimer const& timer);
    void Render();
    void StartTracking() { m_tracking = true; }
    void StopTracking() { m_tracking = false; }
    bool IsTracking() { return m_tracking; }
    void TrackingUpdate(float positionX);

private:
    // Cached pointer to device resources.
    std::shared_ptr<DX::DeviceResources> m_deviceResources;

    bool m_tracking;
};

