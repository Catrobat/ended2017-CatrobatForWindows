#pragma once

class AccelerometerProvider
{
public:
	AccelerometerProvider();
	virtual ~AccelerometerProvider();

	double GetX();
	double GetY();
	double GetZ();

private:
	Windows::Devices::Sensors::Accelerometer^ m_accelerometer;

	bool Init();

	bool m_sensorIsRunningOnDevice;

};

