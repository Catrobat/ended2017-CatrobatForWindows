#include "pch.h"
#include "CompassProvider.h"
#include "PlayerException.h"
#include "DeviceInformation.h"

using namespace Windows::UI::Core;
using namespace Windows::Devices::Sensors;
using namespace Windows::Foundation;

CompassProvider::CompassProvider() 
{
	if (DeviceInformation::IsRunningOnDevice() && Init() == true)
		m_sensorIsRunningOnDevice = true;
	else
		m_sensorIsRunningOnDevice = false;
}

CompassProvider::~CompassProvider()
{
    if (m_compass != nullptr)
    {
        delete m_compass;
    }
}

bool CompassProvider::Init()
{
    bool success = false;
    m_compass = Compass::GetDefault();

    if (m_compass != nullptr)
    {
        uint32 minReportInterval = m_compass->MinimumReportInterval;
        m_compass->ReportInterval = minReportInterval > 16 ? minReportInterval : 16;
        return true;
    }

    return false;
}

float CompassProvider::GetDirection()
{
	float retVal = 0;
	
	if (m_sensorIsRunningOnDevice)
		retVal = 360.0f - static_cast<float>(m_compass->GetCurrentReading()->HeadingMagneticNorth);

    return retVal;
}