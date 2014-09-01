#pragma once

#include <D3D11.h>
#include <windows.foundation.h>

using namespace std;
using namespace DirectX;
using namespace Windows::Foundation;

class BaseObject
{
public:
    virtual void Draw(ID2D1DeviceContext1* deviceContext) = 0;

protected:
	BaseObject(float scaleX = 1, float scaleY = 1);

	XMFLOAT2 m_position;
    XMFLOAT2 m_translation;
	XMFLOAT2 m_objectScale;
};
