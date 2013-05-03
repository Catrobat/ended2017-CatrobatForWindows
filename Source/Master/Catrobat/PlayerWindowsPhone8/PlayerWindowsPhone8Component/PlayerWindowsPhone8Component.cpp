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

using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Microsoft::WRL;
using namespace Windows::Phone::Graphics::Interop;
using namespace Windows::Phone::Input::Interop;

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
	
	Project* project = ProjectDaemon::Instance()->getProject();
	SpriteList* sprites = project->getSpriteList();
	for (int i = sprites->Size() - 1; i >= 0; i--)
	{
		D3D11_SHADER_RESOURCE_VIEW_DESC data;

		/*sprites->getSprite(i)->GetCurrentLookData()->Texture()->GetDesc(&data);
		data.ViewDimension.Value*/

		Bounds bounds = sprites->getSprite(i)->getBounds();
		//if (args->CurrentPoint GetIntermediatePoints()->Size > 0)
		{
			int x = args->CurrentPoint->Position.X;
			int y = args->CurrentPoint->Position.Y;

			if (bounds.x <= x && bounds.y <= y && (bounds.x + bounds.width) >= x && (bounds.y + bounds.height) >= y)
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
	// XML
	XMLParser *xml = new XMLParser();
	//Platform::String^ localfolder = Windows::Storage::ApplicationData::Current->LocalFolder->Path;
	//std::wstring fooW(localfolder->Begin());
	//std::string fooA(fooW.begin(), fooW.end());

	//xml->loadXML(fooA + "/Pacman/projectcode.xml");
	ProjectDaemon::Instance()->setProject(xml->getProject());
	free(xml);



	// Initialize Renderer
	m_renderer = ref new Renderer();
	m_renderer->Initialize(device);
	m_renderer->UpdateForWindowSizeChange(WindowBounds.Width, WindowBounds.Height);

	// Initialize Sound
	m_soundmanager = new SoundManager();
	m_soundmanager->Initialize();

	// Restart timer after renderer has finished initializing.
	m_timer->Reset();

	return S_OK;
}

void Direct3DBackground::Disconnect()
{
	free(m_soundmanager);
	m_soundmanager = nullptr;
	m_renderer = nullptr;
}

HRESULT Direct3DBackground::PrepareResources(_In_ const LARGE_INTEGER* presentTargetTime, _Inout_ DrawingSurfaceSizeF* desiredRenderTargetSize)
{
	m_timer->Update();
	m_renderer->Update(m_timer->Total, m_timer->Delta);
	
	desiredRenderTargetSize->width = ProjectDaemon::Instance()->getProject()->getScreenWidth();
	desiredRenderTargetSize->height = ProjectDaemon::Instance()->getProject()->getScreenHeight();

	return S_OK;
}

HRESULT Direct3DBackground::Draw(_In_ ID3D11Device1* device, _In_ ID3D11DeviceContext1* context, _In_ ID3D11RenderTargetView* renderTargetView)
{
	m_renderer->UpdateDevice(device, context, renderTargetView);
	m_renderer->Render();

	static bool test = false;

	if (!test)
	{
		//std::thread* thr = new std::thread(FMOD_Main);
		//thr->join();
		//FMOD_Main();

		Sound *sound = m_soundmanager->CreateSound(/* Parameters */);
		//sound->Play();

		test = true;
	}

	RequestAdditionalFrame();

	return S_OK;
}

}