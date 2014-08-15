#pragma once

//#include "Direct3DBaseRenderer.h"
#include "SpriteBatch.h"
#include "PrimitiveBatch.h"
#include "SpriteFont.h"
#include "Project.h"
#include "ProjectRenderer.h"
#include "VertexTypes.h" 

ref class ProjectRenderer
{
internal:
	ProjectRenderer();
    //~ProjectRenderer();

	// Direct3DBase methods.
	virtual void CreateDeviceResources();
	virtual void CreateWindowSizeDependentResources();
	virtual void Render();

	// Method for updating time-dependent objects.
	void Update(float timeTotal, float timeDelta);

	void StartUpTasks();

    void Initialize(_In_ ID3D11Device1* device) {/*TODO: implement me*/};

protected private:
	std::unique_ptr<SpriteBatch> m_spriteBatch;
	std::unique_ptr<SpriteFont> m_spriteFont; 
	bool m_Initialized;
};

