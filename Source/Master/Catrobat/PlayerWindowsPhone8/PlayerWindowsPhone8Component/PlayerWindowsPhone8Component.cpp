#include "pch.h"
#include "PlayerWindowsPhone8Component.h"
#include "Direct3DContentProvider.h"
#include "XMLParser.h"
#include "ProjectParser.h"
#include "ProjectDaemon.h"
#include "ScriptHandler.h"
#include "WhenScript.h"
#include "lodepng.h"
#include "lodepng_util.h"
#include "DDSLoader.h"
#include <windows.system.threading.h>
#include <windows.foundation.h>
#include <thread>
#include <math.h>

using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Microsoft::WRL;
using namespace Windows::Phone::Graphics::Interop;
using namespace Windows::Phone::Input::Interop;
using namespace Windows::Graphics::Display;

namespace PhoneDirect3DXamlAppComponent
{

Direct3DBackground::Direct3DBackground() :
	m_timer(ref new BasicTimer())
{
}

IDrawingSurfaceBackgroundContentProvider^ Direct3DBackground::CreateContentProvider()
{
	ComPtr<Direct3DContentProvider> provider = Make<Direct3DContentProvider>(this);
	return reinterpret_cast<IDrawingSurfaceBackgroundContentProvider^>(provider.Detach());
}

// IDrawingSurfaceManipulationHandler
void Direct3DBackground::SetManipulationHost(DrawingSurfaceManipulationHost^ manipulationHost)
{
	manipulationHost->PointerPressed +=
		ref new TypedEventHandler<DrawingSurfaceManipulationHost^, PointerEventArgs^>(this, &Direct3DBackground::OnPointerPressed);

	manipulationHost->PointerMoved +=
		ref new TypedEventHandler<DrawingSurfaceManipulationHost^, PointerEventArgs^>(this, &Direct3DBackground::OnPointerMoved);

	manipulationHost->PointerReleased +=
		ref new TypedEventHandler<DrawingSurfaceManipulationHost^, PointerEventArgs^>(this, &Direct3DBackground::OnPointerReleased);
}

// Event Handlers
void Direct3DBackground::OnPointerPressed(DrawingSurfaceManipulationHost^ sender, PointerEventArgs^ args)
{
	// Insert your code here.
	if (!ProjectDaemon::Instance()->FinishedLoading())
		return;
	Project* project = ProjectDaemon::Instance()->getProject();
	SpriteList* sprites = project->getSpriteList();
	for (int i = sprites->Size() - 1; i >= 0; i--)
	{
		D3D11_SHADER_RESOURCE_VIEW_DESC data;

		/*sprites->getSprite(i)->GetCurrentLookData()->Texture()->GetDesc(&data);
		data.ViewDimension.Value*/

		Bounds bounds = sprites->getSprite(i)->getBounds();
		bounds.x += ProjectDaemon::Instance()->getProject()->getScreenWidth() / 2;
		bounds.y += ProjectDaemon::Instance()->getProject()->getScreenHeight() / 2;
		//if (args->CurrentPoint GetIntermediatePoints()->Size > 0)
		{
			float resolutionScaleFactor;
			switch (DisplayProperties::ResolutionScale) {
				case ResolutionScale::Scale100Percent:
					resolutionScaleFactor = 1.0f;
					break;
				case ResolutionScale::Scale150Percent:
					resolutionScaleFactor = 1.5f;
					break;
				case ResolutionScale::Scale160Percent:
					resolutionScaleFactor = 1.6f;
					break;
			}

			int actualX = args->CurrentPoint->Position.X;
			int actualY = args->CurrentPoint->Position.Y;

			double factorX = abs(ProjectDaemon::Instance()->getProject()->getScreenWidth() / (m_originalWindowsBounds.X / resolutionScaleFactor));
			double factorY = abs(ProjectDaemon::Instance()->getProject()->getScreenHeight() / (m_originalWindowsBounds.Y / resolutionScaleFactor));

			int normalizedX = factorX * actualX;
			int normalizedY = factorY * actualY;		

			if (bounds.x <= normalizedX && bounds.y <= normalizedY && (bounds.x + bounds.width) >= normalizedX && (bounds.y + bounds.height) >= normalizedY)
			{
				for (int j = 0; j < sprites->getSprite(i)->ScriptListSize(); j++)
				{
					Script *script = sprites->getSprite(i)->getScript(j);
					if (script->getType() == Script::TypeOfScript::WhenScript)
					{
						WhenScript *wScript = (WhenScript *) script; 
						if (wScript->getAction() == WhenScript::Action::Tapped)
						{
							wScript->Execute();
						}
					}
					
				}

				// One Hit is enough
				break;
			}
		}
	}

	/*HANDLE ExampleEvent = OpenEvent(EVENT_ALL_ACCESS, FALSE, TEXT("ExampleEvent"));
	SetEvent(ExampleEvent);*/
}

void Direct3DBackground::OnPointerMoved(DrawingSurfaceManipulationHost^ sender, PointerEventArgs^ args)
{
	// Insert your code here.
}

void Direct3DBackground::OnPointerReleased(DrawingSurfaceManipulationHost^ sender, PointerEventArgs^ args)
{
	// Insert your code here.
}

// Interface With Direct3DContentProvider
HRESULT Direct3DBackground::Connect(_In_ IDrawingSurfaceRuntimeHostNative* host, _In_ ID3D11Device1* device)
{
	// Initialize Renderer
	m_renderer = ref new Renderer();
	m_renderer->Initialize(device);

	// Initialize Sound
	SoundManager::Instance()->Initialize();

	//Initialize Project Renderer
	m_projectRenderer = ref new ProjectRenderer();
	ProjectDaemon::Instance()->SetupRenderer(device, m_projectRenderer);

	// Load Project
	ProjectDaemon::Instance()->OpenProject("Piano");

	// Restart timer after renderer has finished initializing.
	m_timer->Reset();

	return S_OK;
}

void Direct3DBackground::Disconnect()
{
	m_renderer = nullptr;
	m_projectRenderer = nullptr;
}

static bool test = false;
HRESULT Direct3DBackground::PrepareResources(_In_ const LARGE_INTEGER* presentTargetTime, _Inout_ DrawingSurfaceSizeF* desiredRenderTargetSize)
{
	m_timer->Update();
	m_renderer->Update(m_timer->Total, m_timer->Delta);
	m_projectRenderer->Update(m_timer->Total, m_timer->Delta);

	// Save this for later
	if (!test)
	{
		m_originalWindowsBounds.X = desiredRenderTargetSize->width;
		m_originalWindowsBounds.Y = desiredRenderTargetSize->height;
		ProjectDaemon::Instance()->SetDesiredRenderTargetSize(desiredRenderTargetSize);
		test = true;
	}
	
	// This would set the desiredRenderTargetSize to the right values BUT we want the loading screen to be 
	// handled with the Device Standard DRTS

	//desiredRenderTargetSize->width = ProjectDaemon::Instance()->getProject()->getScreenWidth();
	//desiredRenderTargetSize->height = ProjectDaemon::Instance()->getProject()->getScreenHeight();

	return S_OK;
}

HRESULT Direct3DBackground::Draw(_In_ ID3D11Device1* device, _In_ ID3D11DeviceContext1* context, _In_ ID3D11RenderTargetView* renderTargetView)
{
	if (!ProjectDaemon::Instance()->FinishedLoading())
	{
		// Render Loading Screen
		m_renderer->UpdateDevice(device, context, renderTargetView);
		m_renderer->Render();
	}
	else
	{
		// Render Project
		m_projectRenderer->UpdateDevice(device, context, renderTargetView);
		m_projectRenderer->Render();
	}

	RequestAdditionalFrame();

	return S_OK;
}

}