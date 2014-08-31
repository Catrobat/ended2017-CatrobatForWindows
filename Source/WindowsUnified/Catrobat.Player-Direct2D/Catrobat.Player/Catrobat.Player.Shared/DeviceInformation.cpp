#include "pch.h"
#include "DeviceInformation.h"

String^ DeviceInformation::GetProcessorArchitecture(void)
{
    SYSTEM_INFO sysInfo;
    String^ str;
    GetNativeSystemInfo(&sysInfo);
    switch (sysInfo.wProcessorArchitecture)
    {
    case PROCESSOR_ARCHITECTURE_AMD64:
        str = "x64";
        break;
    case PROCESSOR_ARCHITECTURE_ARM:
        str = "ARM";
        break;
    case PROCESSOR_ARCHITECTURE_IA64:
        str = "IA64";
        break;
    case PROCESSOR_ARCHITECTURE_INTEL:
        str = "x86";
        break;
    default:
        str = "Unknown";
        break;
    }
    return str;
}

bool DeviceInformation::IsRunningOnDevice(void)
{
    if (GetProcessorArchitecture() == "ARM")
        return true;
    return false;
}
