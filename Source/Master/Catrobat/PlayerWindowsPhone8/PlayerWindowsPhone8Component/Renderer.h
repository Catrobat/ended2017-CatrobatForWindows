#pragma once

#include "Direct3DBase.h"
#include "SpriteBatch.h"

#include <wrl/client.h>

using namespace std;
using namespace DirectX;

ref class Renderer sealed : public Direct3DBase
{
public:
	Renderer();

	// Direct3DBase methods.
	virtual void CreateDeviceResources() override;
	virtual void CreateWindowSizeDependentResources() override;
	virtual void Render() override;
	
	// Method for updating time-dependent objects.
	void Update(float timeTotal, float timeDelta);

private:
	bool m_loadingComplete;
	uint32 m_indexCount;

	// SpriteBatch for Drawing (this is needed by all Object Draw Methods -> Use: m_spriteBatch.Get())
	unique_ptr<SpriteBatch> m_spriteBatch;

	// Use this scale if you calucalte positions on the screen
	float m_scale;
};
