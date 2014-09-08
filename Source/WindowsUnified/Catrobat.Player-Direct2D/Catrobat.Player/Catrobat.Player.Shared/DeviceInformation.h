#pragma once

class DeviceInformation
{
public:
    static Platform::String^ GetProcessorArchitecture(void);
    static bool IsRunningOnDevice(void);
};

