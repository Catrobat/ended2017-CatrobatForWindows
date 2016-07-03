#pragma once

#include <ppltasks.h>
#include <time.h>
#include "WASAPICapture.h"
#include "DeviceState.h"
#include "AudioData.h"

using namespace Catrobat_Player;

ref class LoudnessCapture sealed
{
public:

    LoudnessCapture();

    double GetLoudness();
    bool StartCapture();
    bool StopCapture();

    void UpdateLoudness(int value);

private:

    double m_loudness;

    // Handlers
    void OnDeviceStateChange(Object^ sender, DeviceStateChangedEventArgs^ e);
    void OnAudioDataReady(Object^ sender, AudioDataReadyEventArgs^ e);

    Windows::Foundation::EventRegistrationToken     m_deviceStateChangeToken;
    Windows::Foundation::EventRegistrationToken     m_audioDataReadyToken;

    DeviceStateChangedEvent^                        m_StateChangedEvent;
    ComPtr<WASAPICapture>                           m_spCapture;
    Windows::UI::Core::CoreDispatcher^              m_CoreDispatcher;

};

