#include "pch.h"
#include "AccelerometerProvider.h"
#include "PlayerException.h"

AccelerometerProvider::AccelerometerProvider()
{
	if (!Init())
		throw new PlayerException("init inclination provider failed");
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
	AccelerometerReading^ reading = m_accelerometer->GetCurrentReading(); 

	if (reading == nullptr)
		return 0;
	return reading->AccelerationX;
}

double AccelerometerProvider::GetY()
{
	AccelerometerReading^ reading = m_accelerometer->GetCurrentReading(); 

	if (reading == nullptr)
		return 0;
	return reading->AccelerationY;
}

double AccelerometerProvider::GetZ()
{
	AccelerometerReading^ reading = m_accelerometer->GetCurrentReading(); 

	if (reading == nullptr)
		return 0;
	return reading->AccelerationZ;
}

