#pragma once

ref class InclinationProvider sealed
{
public:
	InclinationProvider();
	virtual ~InclinationProvider();

	// return value of rotation about x-axis [-180;180] degree
	float GetPitch();

	// return value of rotation about y-axis [-180;180] degree
	float GetRoll();

	// return value of rotation about z-axis [0;360] degree
	// not part of the android version!!!
	float GetYaw();

private:

	Windows::Devices::Sensors::Inclinometer^ m_inclinometer;
	bool Init();

	bool m_sensorIsRunningOnDevice;

};

