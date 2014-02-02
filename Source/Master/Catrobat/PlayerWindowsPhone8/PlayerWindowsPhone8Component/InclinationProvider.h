#pragma once

using namespace Windows::Devices::Sensors;

class InclinationProvider
{
public:
	InclinationProvider();
	~InclinationProvider();
	float GetPitch();
	float GetRoll();
	float GetYaw();

private:

	Inclinometer^ m_inclinometer;
	bool Init();

};

