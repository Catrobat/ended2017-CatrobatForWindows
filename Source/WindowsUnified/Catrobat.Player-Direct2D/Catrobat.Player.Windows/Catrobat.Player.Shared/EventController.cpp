#include "pch.h"
#include "EventController.h"
#include "ProjectDaemon.h"

using namespace Windows::Graphics::Display;
using namespace Windows::UI::Core;


EventController::EventController()
{
}


// Event Handlers
//--------------------------------------------------------------------------------------

void EventController::OnPointerPressed(
	_In_ CoreWindow^ sender,
	_In_ PointerEventArgs^ args
	)
{
	//if (!ProjectDaemon::Instance()->FinishedLoading())
	//{
	//	return;
	//}

	//Project* project = ProjectDaemon::Instance()->GetProject();
	//ObjectList* objects = project->GetObjectList();

	//if (objects == NULL)
	//{
	//	return;
	//}

	//bool objectFound = false;

	//for (int i = objects->GetSize() - 1; i >= 0; i--)
	//{
	//	try
	//	{
	//		float resolutionScaleFactor;

	//		switch (DisplayProperties::ResolutionScale)
	//		{
	//		case ResolutionScale::Scale100Percent:
	//			resolutionScaleFactor = 1.0f;
	//			break;
	//		case ResolutionScale::Scale150Percent:
	//			resolutionScaleFactor = 1.5f;
	//			break;
	//		case ResolutionScale::Scale160Percent:
	//			resolutionScaleFactor = 1.6f;
	//			break;
	//		}

	//		double actualX = args->CurrentPoint->Position.X;
	//		double actualY = args->CurrentPoint->Position.Y;

	//		double factorX = abs(ProjectDaemon::Instance()->GetProject()->GetScreenWidth() / (m_originalWindowsBounds.X / resolutionScaleFactor));
	//		double factorY = abs(ProjectDaemon::Instance()->GetProject()->GetScreenHeight() / (m_originalWindowsBounds.Y / resolutionScaleFactor));

	//		double normalizedX = actualX * ((int)DisplayProperties::ResolutionScale) / 100.0;
	//		double normalizedY = actualY * ((int)DisplayProperties::ResolutionScale) / 100.0;

	//		auto current = objects->GetObject(i);

	//		if (current->IsTapPointHitting(m_context, m_device, actualX, actualY, (double)((int)DisplayProperties::ResolutionScale) / 100.0))
	//		{
	//			for (int j = 0; j < current->GetScriptListSize(); j++)
	//			{
	//				Script *script = current->GetScript(j);

	//				if (script->GetType() == Script::TypeOfScript::WhenScript)
	//				{
	//					if (current->GetWhenScript() != NULL && current->GetWhenScript()->IsRunning())
	//					{
	//						return;
	//					}
	//					else
	//					{
	//						WhenScript *whenScript = (WhenScript *)script;

	//						if (whenScript->GetAction() == WhenScript::Action::Tapped)
	//						{
	//							current->SetWhenScript(whenScript);
	//							whenScript->Execute();
	//							objectFound = true;
	//							break;
	//						}
	//					}
	//				}
	//			}
	//		}

	//		if (objectFound)
	//			break;
	//	}
	//	catch (PlayerException *e) //TODO: Exception handling
	//	{
	//	}
	//	catch (Platform::Exception ^e) //TODO: Exception handling
	//	{
	//	}
	//}
}

//--------------------------------------------------------------------------------------

void EventController::OnPointerMoved(
	_In_ CoreWindow^ sender,
	_In_ PointerEventArgs^ args
	)
{
	// TODO: implement me
}

//--------------------------------------------------------------------------------------

void EventController::OnPointerReleased(
	_In_ CoreWindow^ sender,
	_In_ PointerEventArgs^ args
	)
{
	// TODO: implement me
}

//--------------------------------------------------------------------------------------

void EventController::OnPointerExited(
	_In_ CoreWindow^ sender,
	_In_ PointerEventArgs^ args
	)
{
	// TODO: implement me
}


