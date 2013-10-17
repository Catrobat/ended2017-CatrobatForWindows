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
        // Select a report interval that is both suitable for the purposes of the app and supported by the sensor.
        // This value will be used later to activate the sensor.
        uint32 minReportInterval = m_compass->MinimumReportInterval;
        m_desiredReportInterval = minReportInterval > 16 ? minReportInterval : 16;
        success = true;
    }
    else
    {
        //rootPage->NotifyUser("No compass found", NotifyType::ErrorMessage);
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
        // Establish the report interval
        m_compass->ReportInterval = m_desiredReportInterval;

        //auto visibilityToken = Window::Current->VisibilityChanged::add(ref new WindowVisibilityChangedEventHandler(this, &Scenario1::VisibilityChanged));
        m_compass->GetCurrentReading();

        success = true;
    }
    else
    {
        //rootPage->NotifyUser("No compass found", NotifyType::ErrorMessage);
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
    
    auto blub = 0;
}
//if (reading->HeadingTrueNorth != nullptr)
//{
//    ScenarioOutput_TrueNorth->Text = reading->HeadingTrueNorth->Value.ToString();
//}
//else
//{
//    ScenarioOutput_TrueNorth->Text = "No data";
//}
//switch (reading->HeadingAccuracy)
//{
//case MagnetometerAccuracy::Unknown:
//    ScenarioOutput_HeadingAccuracy->Text = "Unknown";
//    break;
//case MagnetometerAccuracy::Unreliable:
//    ScenarioOutput_HeadingAccuracy->Text = "Unreliable";
//    break;
//case MagnetometerAccuracy::Approximate:
//    ScenarioOutput_HeadingAccuracy->Text = "Approximate";
//    break;
//case MagnetometerAccuracy::High:
//    ScenarioOutput_HeadingAccuracy->Text = "High";
//    break;
//default:
//    ScenarioOutput_HeadingAccuracy->Text = "No data";
//    break;
//}
//}