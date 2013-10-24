#pragma once

using namespace Windows::Devices::Sensors;

class CompassProvider
{
public:
    CompassProvider();
    ~CompassProvider();
    bool Init();
    float GetDirection();

private:

    Compass^ m_compass;
};

