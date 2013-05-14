#pragma once

#include "Direct3DBase.h"

ref class ProjectRenderer sealed : public Direct3DBase
{
public:
	ProjectRenderer();

	// Direct3DBase methods.
	virtual void CreateDeviceResources() override;
	virtual void CreateWindowSizeDependentResources() override;
	virtual void Render() override;

	// Method for updating time-dependent objects.
	void Update(float timeTotal, float timeDelta);
};

