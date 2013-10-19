#pragma once

using namespace Windows::Devices::Sensors;

class CompassProvider
{
public:
    CompassProvider();
    bool Init();
    bool Start();
    void Stop();
    float GetDirection();
    bool IsStarted();

private:

    Windows::UI::Core::CoreDispatcher^ m_dispatcher;
    Windows::Foundation::EventRegistrationToken m_visibilityToken;
    uint32 m_desiredReportInterval;
    Compass^ m_compass;
    float m_currentDirection;
    Windows::Foundation::EventRegistrationToken m_readingToken;

    void ReadingChanged(Windows::Devices::Sensors::Compass^ sender, Windows::Devices::Sensors::CompassReadingChangedEventArgs^ e);
    bool m_isStarted;
};

