#pragma once

ref class EventController abstract
{

internal:

	EventController();

protected:

	void OnPointerPressed(
		_In_ Windows::UI::Core::CoreWindow^ sender,
		_In_ Windows::UI::Core::PointerEventArgs^ args
		);
	void OnPointerMoved(
		_In_ Windows::UI::Core::CoreWindow^ sender,
		_In_ Windows::UI::Core::PointerEventArgs^ args
		);
	void OnPointerReleased(
		_In_ Windows::UI::Core::CoreWindow^ sender,
		_In_ Windows::UI::Core::PointerEventArgs^ args
		);
	void OnPointerExited(
		_In_ Windows::UI::Core::CoreWindow^ sender,
		_In_ Windows::UI::Core::PointerEventArgs^ args
		);

};

