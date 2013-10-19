#include "pch.h"
#include "CompassProvider.h"

using namespace Windows::UI::Core;
using namespace Windows::Devices::Sensors;
using namespace Windows::Foundation;

CompassProvider::CompassProvider() { }

bool CompassProvider::Init()
{
    bool success = false;

    m_compass = Compass::GetDefault();
    if (m_compass != nullptr)
    {
        uint32 minReportInterval = m_compass->MinimumReportInterval;
        m_desiredReportInterval = minReportInterval > 16 ? minReportInterval : 16;
        success = true;
    }
    else
    {
        success = false;
    }

    success &= Start();
    return success;
}

bool CompassProvider::IsStarted()
{
    return m_isStarted;
}

bool CompassProvider::Start()
{
    bool success = false;
    
    if (m_compass != nullptr)
    {
        m_compass->ReportInterval = m_desiredReportInterval;
        m_compass->GetCurrentReading();

        success = true;
    }
    else
    {
        success = false;
    }

    m_isStarted = success;

    return success;
}

void CompassProvider::Stop()
{
    m_compass->ReadingChanged::remove(m_readingToken);
    m_compass->ReportInterval = 0;
    m_isStarted = false;
}

float CompassProvider::GetDirection()
{
    float retVal = m_compass->GetCurrentReading()->HeadingMagneticNorth;
    return retVal;
}

void CompassProvider::ReadingChanged(Compass^ sender, CompassReadingChangedEventArgs^ e)
{
    CompassReading^ reading = e->Reading;
    m_currentDirection = reading->HeadingMagneticNorth;
}