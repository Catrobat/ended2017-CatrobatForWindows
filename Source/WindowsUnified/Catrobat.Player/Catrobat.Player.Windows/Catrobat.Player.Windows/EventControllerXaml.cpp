#include "pch.h"
#include "EventControllerXaml.h"

using namespace Windows::UI::Core;
using namespace Windows::UI::Input;
using namespace Windows::UI;
using namespace Windows::Foundation;
using namespace Microsoft::WRL;
using namespace DirectX;
using namespace Windows::Devices::Input;
using namespace Windows::System;


//----------------------------------------------------------------------

EventController^ EventControllerXaml::Create(_In_ CoreWindow^ window,
	_In_ CoreDispatcher^ dispatcher
	)
{
	auto p = ref new EventControllerXaml(window, dispatcher);
	return static_cast<EventController^>(p);
}

//----------------------------------------------------------------------

EventControllerXaml::EventControllerXaml(
	_In_ CoreWindow^ window,
	_In_ CoreDispatcher ^dispatcher
	) :
	m_isControllerConnected(false)
{
	// Even though all current realizations of MoveLookController install the
	// PointerPressed, PointerMoved, PointerReleased and PointerExited event
	// handlers, it was decided to put all event handler registrations together
	// in the constructor.

	// The windows version of the MoveLookController installs event handlers
	// for keyboard input and for relative mouse movement.  There are two
	// parts required to enable relative mouse movement, the event handler
	// and disabling the cursor pointer.  The game is running on a separate thread
	// from the Xaml UI thread, and this separate thread does NOT have access to
	// the CoreWindow.  The dispatcher can be used to marshal execution back to
	// the Xaml UI thread which does have a CoreWindow.  The Dispatcher is cached
	// to enable execution of code on the UI thread to turn on and off the cursor glyph.

	m_dispatcher = dispatcher;

	window->PointerPressed +=
		ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &EventControllerXaml::OnPointerPressed);

	window->PointerMoved +=
		ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &EventControllerXaml::OnPointerMoved);

	window->PointerReleased +=
		ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &EventControllerXaml::OnPointerReleased);

	window->PointerExited +=
		ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &EventControllerXaml::OnPointerExited);

}
