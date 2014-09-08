#pragma once

#include <D3D11.h>
#include <windows.foundation.h>

class BaseObject
{
public:
    virtual void Draw(const std::shared_ptr<DX::DeviceResources>& deviceResources) = 0;

protected:
	BaseObject(float scaleX = 1, float scaleY = 1);

	D2D1_POINT_2F m_position;
    D2D1_POINT_2F m_translation;
    D2D1_SIZE_F m_objectScale;
};
