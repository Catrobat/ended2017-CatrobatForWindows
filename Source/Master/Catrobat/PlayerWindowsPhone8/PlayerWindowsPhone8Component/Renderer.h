#pragma once

#include "Direct3DBase.h"
#include "SpriteBatch.h"
#include "SpriteFont.h"
#include "Project.h"

#include <wrl/client.h>

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
	bool m_startup;
	uint32 m_indexCount;

	// SpriteBatch for Drawing (this is needed by all Object Draw Methods -> Use: m_spriteBatch.Get())
	unique_ptr<SpriteBatch> m_spriteBatch;
	std::unique_ptr<SpriteFont> m_spriteFont; 

	// Use this scale if you calucalte positions on the screen
	float m_scale;

	void StartUpTasks();

	// Just for testing
	void CreateTestObject2();
	void CreateTestObject3();
	void CreateTestObject4();
};
