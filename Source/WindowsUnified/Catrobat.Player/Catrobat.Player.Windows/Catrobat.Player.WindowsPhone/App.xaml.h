//
// App.xaml.h
// Declaration of the App class.
//

#pragma once

#if defined(_DEBUG)
#define DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION 1
#endif

#include "App.g.h"
#include "Player.xaml.h"
#include "Test01.xaml.h"

namespace Catrobat_Player
{
    [Windows::Foundation::Metadata::WebHostHidden]
    public ref class App sealed
    {
    public:
        App();

        virtual void OnLaunched(
            _In_ Windows::ApplicationModel::Activation::LaunchActivatedEventArgs^ args
            ) override;

    private:
        void OnSuspending(
            _In_ Platform::Object^ sender,
            _In_ Windows::ApplicationModel::SuspendingEventArgs^ args
            );

        void OnResuming(
            _In_ Platform::Object^ sender,
            _In_ Platform::Object^ args
            );

        void OnWindowActivationChanged(
            _In_ Platform::Object^ sender,
            _In_ Windows::UI::Core::WindowActivatedEventArgs^ args
            );

        Player^ m_mainPage;
		//Test01^ m_mainPage;
    };
}
