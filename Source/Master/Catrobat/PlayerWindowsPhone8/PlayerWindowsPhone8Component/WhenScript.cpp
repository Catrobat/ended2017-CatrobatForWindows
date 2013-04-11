#include "pch.h"
#include "WhenScript.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;
WhenScript::WhenScript(string action, string spriteReference, Sprite *parent) :
	Script(TypeOfScript::WhenScript, spriteReference, parent), m_action(action)
{
}

string WhenScript::getAction()
{
	return m_action;
}

void WhenScript::Execute()
{
	Platform::String ^EventName = "ExampleEvent";
	HANDLE ExampleEvent = CreateEventEx(NULL, TEXT("ExampleEvent"), CREATE_EVENT_MANUAL_RESET, EVENT_ALL_ACCESS );

	Core::SignalNotifier ^NamedEventNotifier = Core::SignalNotifier::AttachToEvent(EventName,
		ref new Core::SignalHandler([this] (Core::SignalNotifier ^signalNotifier, bool timedOut)
	{
		list<Brick*>::iterator it;
		for(it=m_brickList->begin(); it!=m_brickList->end(); it++)
		{
			(*it)->Execute();
		}

	}));

	NamedEventNotifier->Enable();
}