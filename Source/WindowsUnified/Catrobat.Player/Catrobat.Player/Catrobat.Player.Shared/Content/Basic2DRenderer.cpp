#include "pch.h"
#include "Basic2DRenderer.h"
#include "TextureDaemon.h"
#include "ProjectDaemon.h"

#include "Common/DirectXHelper.h"

#include <windows.h>
#include <wincodec.h>

using namespace std;

Basic2DRenderer::Basic2DRenderer(const std::shared_ptr<DX::DeviceResources>& deviceResources) :
m_deviceResources(deviceResources)
{
    CreateDeviceDependentResources();
    ProjectDaemon::Instance()->GetProject()->StartUp();
}

void Basic2DRenderer::Update(DX::StepTimer const& timer)
{
}

void Basic2DRenderer::Render()
{
    auto deviceContext = m_deviceResources->GetD2DDeviceContext();

    //begin drawing operations, draw bitmap, end drawing
    deviceContext->BeginDraw();
    ProjectDaemon::Instance()->GetProject()->Render(m_deviceResources);
    deviceContext->EndDraw();
}

void Basic2DRenderer::CreateDeviceDependentResources()
{
    ProjectDaemon::Instance()->GetProject()->LoadTextures(m_deviceResources);
}

void Basic2DRenderer::CreateWindowSizeDependentResources()
{
    ProjectDaemon::Instance()->GetProject()->SetupWindowSizeDependentResources(m_deviceResources);
}


void Basic2DRenderer::ReleaseDeviceDependentResources()
{

}

void Basic2DRenderer::PointerPressed(D2D1_POINT_2F point)
{
	for each (pair<string, shared_ptr<Object>> var in ProjectDaemon::Instance()->GetProject()->GetObjectList())
	{
		if (var.second->IsObjectHit(point))
		{
			for (int scriptIndex = 0; scriptIndex < var.second->GetScriptListSize(); scriptIndex++)
			{
				shared_ptr<Script> script = var.second->GetScript(scriptIndex);
				if (script->GetType() == Script::TypeOfScript::WhenScript)
				{
					shared_ptr<WhenScript> whenScript = dynamic_pointer_cast<WhenScript>(script);
					if (whenScript->GetAction() == WhenScript::Action::Tapped)
					{
						whenScript->Execute();
					}
				}
			}
			break;
		}
	}
}
