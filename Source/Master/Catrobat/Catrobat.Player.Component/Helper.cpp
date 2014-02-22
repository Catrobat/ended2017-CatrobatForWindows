#include "pch.h"
#include "Helper.h"

string Helper::ConvertPlatformStringToString(String^ input)
{
	wstring foo(input->Begin());
	string res(foo.begin(), foo.end());
	return res;
}

String^ Helper::ConvertStringToPlatformString(string input)
{
    std::string s_str = std::string(input);
    std::wstring wid_str = std::wstring(s_str.begin(), s_str.end());
    const wchar_t* w_char = wid_str.c_str();
    return ref new Platform::String(w_char);
}

string Helper::RetrieveClassName(string input)
{
    string className = input.erase(0, input.find(" "));
    return className;
}

String^ Helper::GetProcessorArchitecture(void)
{
    SYSTEM_INFO sysInfo;
    String^ str;
    GetNativeSystemInfo(&sysInfo);
    switch (sysInfo.wProcessorArchitecture)
    {
    case PROCESSOR_ARCHITECTURE_AMD64:
        str = "x64"; //AMD or Intel
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

bool Helper::IsRunningOnDevice(void)
{
    if (GetProcessorArchitecture() == "ARM")
        return true;
    return false;
}