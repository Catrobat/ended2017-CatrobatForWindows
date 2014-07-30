#include "pch.h"
#include "EventControllerXaml.h"

using namespace Windows::UI::Core;
using namespace Windows::Foundation;
using namespace Windows::Phone::UI::Input;


//----------------------------------------------------------------------

EventControllerXaml::EventControllerXaml(_In_ Windows::UI::Core::CoreWindow^ window)
{
	window->PointerPressed +=
		ref new TypedEventHandler<Windows::UI::Core::CoreWindow^, PointerEventArgs^>(this, &EventControllerXaml::OnPointerPressed);

	window->PointerMoved +=
		ref new TypedEventHandler<Windows::UI::Core::CoreWindow^, PointerEventArgs^>(this, &EventControllerXaml::OnPointerMoved);

	window->PointerReleased +=
		ref new TypedEventHandler<Windows::UI::Core::CoreWindow^, PointerEventArgs^>(this, &EventControllerXaml::OnPointerReleased);

	window->PointerExited +=
		ref new TypedEventHandler<Windows::UI::Core::CoreWindow^, PointerEventArgs^>(this, &EventControllerXaml::OnPointerExited);

	HardwareButtons::BackPressed +=
		ref new EventHandler<BackPressedEventArgs^>(this, &EventControllerXaml::OnHardwareBackButtonPressed);
}

//----------------------------------------------------------------------

EventController^ EventControllerXaml::Create(_In_ Windows::UI::Core::CoreWindow^ window,
	_In_ CoreDispatcher^ /* dispatcher */
	)
{
	auto p = ref new EventControllerXaml(window);
	return static_cast<EventController^>(p);
}

//----------------------------------------------------------------------

void EventControllerXaml::OnHardwareBackButtonPressed(
	_In_ Platform::Object^ sender,
	BackPressedEventArgs ^args
	)
{
	// TODO: implement me

	//if (m_state == MoveLookControllerState::Active)
	//{
	//	// The game is currently in active play mode, so hitting the hardware back button
	//	// will cause the game to pause.
	//	//m_pausePressed = true;
	//	args->Handled = false;
	//}
	//else
	//{
	//	// The game is not currently in active play mode, so take the default behavior
	//	// for the hardware back button.
	//	args->Handled = false;
	//}
}
