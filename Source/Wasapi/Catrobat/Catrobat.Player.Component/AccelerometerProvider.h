#pragma once
using namespace Windows::Devices::Sensors;

class AccelerometerProvider
{
public:
	AccelerometerProvider();
	virtual ~AccelerometerProvider();

	double GetX();
	double GetY();
	double GetZ();

private:
	Accelerometer^ m_accelerometer;

	bool Init();

};

