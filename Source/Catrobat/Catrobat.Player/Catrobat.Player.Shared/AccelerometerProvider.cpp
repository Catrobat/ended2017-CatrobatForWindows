#include "pch.h"
#include "AccelerometerProvider.h"
#include "PlayerException.h"
#include "DeviceInformation.h"

using namespace Windows::Devices::Sensors;

AccelerometerProvider::AccelerometerProvider()
{
	if (DeviceInformation::IsRunningOnDevice() && Init() == true)
		m_sensorIsRunningOnDevice = true;
	else
		m_sensorIsRunningOnDevice = false;
}

AccelerometerProvider::~AccelerometerProvider()
{
}

bool AccelerometerProvider::Init()
{
	auto success = false;
	m_accelerometer = Accelerometer::GetDefault();

	if (m_accelerometer != nullptr)
	{
		success = true;
	}
	return success;
}

double AccelerometerProvider::GetX()
{
	if (m_sensorIsRunningOnDevice)
	{
		AccelerometerReading^ reading = m_accelerometer->GetCurrentReading();

		if (reading == nullptr)
			return 0;
		return reading->AccelerationX;
	}

	return 0;
}

double AccelerometerProvider::GetY()
{
	if (m_sensorIsRunningOnDevice)
	{
		AccelerometerReading^ reading = m_accelerometer->GetCurrentReading();

		if (reading == nullptr)
			return 0;
		return reading->AccelerationY;
	}

	return 0;
}

double AccelerometerProvider::GetZ()
{
	if (m_sensorIsRunningOnDevice)
	{
		AccelerometerReading^ reading = m_accelerometer->GetCurrentReading();

		if (reading == nullptr)
			return 0;
		return reading->AccelerationZ;
	}

	return 0;
}

