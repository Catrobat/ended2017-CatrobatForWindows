#include "pch.h"
#include "InclinationProvider.h"
#include "PlayerException.h"
#include "DeviceInformation.h"

using namespace Windows::UI::Core;
using namespace Windows::Devices::Sensors;
using namespace Windows::Foundation;

InclinationProvider::InclinationProvider()
{
	if (DeviceInformation::IsRunningOnDevice() && Init() == true)
		m_sensorIsRunningOnDevice = true;
	else
		m_sensorIsRunningOnDevice = false;
}

InclinationProvider::~InclinationProvider()
{
    if (m_inclinometer != nullptr)
    {
        delete m_inclinometer;
    }
}

bool InclinationProvider::Init()
{
    auto success = false;
    m_inclinometer = Inclinometer::GetDefault();

    if (m_inclinometer != nullptr)
    {
        success = true;
    }

    return success;
}

float InclinationProvider::GetPitch()
{
	float retVal = 0; 
	
	if (m_sensorIsRunningOnDevice)
		retVal = m_inclinometer->GetCurrentReading()->PitchDegrees;

    return retVal;
}

float InclinationProvider::GetRoll()
{
	float retVal = 0;
	
	if (m_sensorIsRunningOnDevice)
	{
		retVal = m_inclinometer->GetCurrentReading()->RollDegrees;
		retVal = -retVal; // to be compatible with android version
	}

    return retVal;
}

float InclinationProvider::GetYaw()
{
	float retVal = 0;
	
	if (m_sensorIsRunningOnDevice)
		retVal = m_inclinometer->GetCurrentReading()->YawDegrees;

    return retVal;
}

