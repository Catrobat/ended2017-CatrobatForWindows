#include "pch.h"
#include "InclinationProvider.h"
#include "PlayerException.h"

using namespace Windows::UI::Core;
using namespace Windows::Devices::Sensors;
using namespace Windows::Foundation;

InclinationProvider::InclinationProvider()
{
	if (Init() != true)
		throw new PlayerException("init inclination provider failed");
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
	bool success = false;
	m_inclinometer = Inclinometer::GetDefault();

	if (m_inclinometer != nullptr)
	{
		uint32 minReportInterval = m_inclinometer->MinimumReportInterval;
		m_inclinometer->ReportInterval = minReportInterval > 16 ? minReportInterval : 16; //TODO: introduce constant...
		success = true;
	}

	return success;
}

float InclinationProvider::GetPitch()
{
	float retVal = 0;// m_inclinometer->GetCurrentReading()->PitchDegrees;
	return retVal;
}

float InclinationProvider::GetRoll()
{
	float retVal = 0;// m_inclinometer->GetCurrentReading()->RollDegrees;
	return retVal;
}

float InclinationProvider::GetYaw()
{
	float retVal = 0;// m_inclinometer->GetCurrentReading()->YawDegrees;
	return retVal;
}

