#pragma once
#include <string>

using namespace std;
using namespace Platform;

class DeviceInformation
{
public:
    static String^ GetProcessorArchitecture(void);
    static bool IsRunningOnDevice(void);
};

