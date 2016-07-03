#include "pch.h"
#include "LoudnessCapture.h"
#include "DeviceInformation.h"
#include "PlayerException.h"

#include <string.h>
#include <sstream>
#include <math.h>

using namespace std;

using namespace Windows::System;
using namespace Windows::System::Threading;
using namespace Windows::Foundation;
using namespace Platform;
using namespace Windows::UI::Core;

LoudnessCapture::LoudnessCapture() :
    m_StateChangedEvent(nullptr),
    m_spCapture(nullptr)
{
    m_loudness = 0.0;
    m_CoreDispatcher = CoreWindow::GetForCurrentThread()->Dispatcher;
    // Initialize MF
    HRESULT hr = MFStartup(MF_VERSION, MFSTARTUP_LITE);
    if (!SUCCEEDED(hr))
    {
        ThrowIfFailed(hr);
    }
}

double LoudnessCapture::GetLoudness()
{
    return m_loudness;
}

bool LoudnessCapture::StartCapture()
{
    if (m_spCapture)
    {
        m_spCapture = nullptr;
    }
    // Create a new WASAPI capture instance
    m_spCapture = Make<WASAPICapture, bool>(false);

    if (nullptr == m_spCapture)
    {
        OnDeviceStateChange(this, ref new DeviceStateChangedEventArgs(DeviceState::DeviceStateInError, E_OUTOFMEMORY));
        return false;
    }

    // Register for events
    m_deviceStateChangeToken = DeviceStateChangedEvent::StateChangedEvent += ref new DeviceStateChangedHandler(this, &LoudnessCapture::OnDeviceStateChange);
    m_audioDataReadyToken = AudioDataReadyEvent::AudioDataReady += ref new AudioDataReadyHandler(this, &LoudnessCapture::OnAudioDataReady);

    // Perform the initialization
    m_spCapture->InitializeAudioDeviceAsync();

    return true;
}

bool LoudnessCapture::StopCapture()
{
    if (m_spCapture)
    {
        m_spCapture->StopCaptureAsync();
        return true;
    }
    return false;
}

void LoudnessCapture::UpdateLoudness(int value)
{
    this->m_loudness = value;
}

void LoudnessCapture::OnDeviceStateChange(Object^ sender, DeviceStateChangedEventArgs^ e)
{
    // Handle state specific messages
    switch (e->State)
    {
    case DeviceState::DeviceStateInitialized:
        m_spCapture->StartCaptureAsync();
        break;

    case DeviceState::DeviceStateCapturing:
    case DeviceState::DeviceStateDiscontinuity:
    case DeviceState::DeviceStateFlushing:
        break;

    case DeviceState::DeviceStateStopped:
        // For the stopped state, completely tear down the audio device
        m_spCapture = nullptr;

        if (m_deviceStateChangeToken.Value != 0)
        {
            m_StateChangedEvent->StateChangedEvent -= m_deviceStateChangeToken;
            m_StateChangedEvent = nullptr;
            m_deviceStateChangeToken.Value = 0;
        }
        break;

    case DeviceState::DeviceStateInError:
        HRESULT hr = e->hr;

        if (m_deviceStateChangeToken.Value != 0)
        {
            m_StateChangedEvent->StateChangedEvent -= m_deviceStateChangeToken;
            m_StateChangedEvent = nullptr;
            m_deviceStateChangeToken.Value = 0;
        }

        m_spCapture = nullptr;

        wchar_t hrVal[11];
        swprintf_s(hrVal, 11, L"0x%08x\0", hr);
        String^ strHRVal = ref new String(hrVal);

        // Specifically handle a couple of known errors
        switch (hr)
        {
        case __HRESULT_FROM_WIN32(ERROR_FILE_NOT_FOUND):
            break;

        case AUDCLNT_E_RESOURCES_INVALIDATED:
            break;

        case E_ACCESSDENIED:
            break;

        default:
            break;
        }
    }
}

/// To measure the energy we use the Root-Mean-Square
void LoudnessCapture::OnAudioDataReady(Object^ sender, AudioDataReadyEventArgs^ e)
{
    double rms = 0;
    int volume = 0;

    for (int i = 0; i < e->Size - 1; i += 1)
    {
        rms += std::pow(e->PcmData[i], 2);
    }

    rms /= (double)(e->Size / 2);
    volume = (int)std::floor(std::sqrt(rms));

    this->UpdateLoudness(volume);
}
