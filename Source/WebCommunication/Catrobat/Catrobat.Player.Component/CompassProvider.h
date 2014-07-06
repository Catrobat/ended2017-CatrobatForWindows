#pragma once

using namespace Windows::Devices::Sensors;

class CompassProvider
{
public:
    CompassProvider();
    ~CompassProvider();
    float GetDirection();

private:

    Compass^ m_compass;
    bool Init();

	bool m_sensorIsRunningOnDevice;

};

