#pragma once
#include "EventController.h"


ref class EventControllerXaml : public EventController
{

internal:
	static EventController^ Create(
		_In_ Windows::UI::Core::CoreWindow^ window,
		_In_ Windows::UI::Core::CoreDispatcher^ dispatcher
		);

private:
	EventControllerXaml(
		_In_ Windows::UI::Core::CoreWindow^ window,
		_In_ Windows::UI::Core::CoreDispatcher^ dispatcher
		);

protected private:
	// Cached pointer to a dispatcher to marshal execution back to the Xaml UI thread.
	Windows::UI::Core::CoreDispatcher^ m_dispatcher;

	// Xbox Input related members.
	bool                        m_isControllerConnected;  // Is the Xbox controller connected.

};

