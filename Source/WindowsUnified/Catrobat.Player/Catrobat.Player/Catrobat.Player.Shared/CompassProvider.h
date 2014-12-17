#pragma once

class CompassProvider
{
public:
    CompassProvider();
    ~CompassProvider();
    float GetDirection();

private:

	Windows::Devices::Sensors::Compass^ m_compass;
    bool Init();

	bool m_sensorIsRunningOnDevice;

};

