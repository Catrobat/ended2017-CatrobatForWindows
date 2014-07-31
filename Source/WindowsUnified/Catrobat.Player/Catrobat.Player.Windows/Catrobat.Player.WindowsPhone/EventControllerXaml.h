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

	EventControllerXaml(_In_ Windows::UI::Core::CoreWindow^ window);

	void OnHardwareBackButtonPressed(
		_In_ Platform::Object^ sender,
		Windows::Phone::UI::Input::BackPressedEventArgs ^args
		);

};

