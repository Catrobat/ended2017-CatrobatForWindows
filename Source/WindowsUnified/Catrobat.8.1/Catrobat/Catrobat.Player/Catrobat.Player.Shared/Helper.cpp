#include "pch.h"
#include <string>
#include "Helper.h"
#include <algorithm>
#include <limits.h>

std::string Helper::StdString(Platform::String^ input)
{
    std::wstring origin(input->Begin());
    std::string result(origin.begin(), origin.end());
	return result;
}

Platform::String^ Helper::PlatformString(std::string input)
{
    std::string s_str = std::string(input);
    std::wstring wid_str = std::wstring(s_str.begin(), s_str.end());
    const wchar_t* w_char = wid_str.c_str();
    return ref new Platform::String(w_char);
}

std::string Helper::RetrieveClassName(std::string input)
{
    std::string className = input.erase(0, input.find(" "));
    return className;
}

bool Helper::ConvertStringToDouble(std::string s, double* d)
{   
    char* check = NULL;

    // TODO to check: unexpected behaviour for DBL_MAX and DBL_MIN in s
    *d = std::strtod(s.c_str(), &check);

    if (check == NULL || *check != '\0' || !(*d <= DBL_MAX && *d >= -DBL_MAX))
        return false;
    
    return true;
}

std::wstring Helper::ConvertStringToLPCWSTR(const std::string& s)
{
	int len;
	int slength = (int)s.length() + 1;
	len = MultiByteToWideChar(CP_ACP, 0, s.c_str(), slength, 0, 0);
	wchar_t* buf = new wchar_t[len];
	MultiByteToWideChar(CP_ACP, 0, s.c_str(), slength, buf, len);
	std::wstring r = std::wstring(buf);
	delete[] buf;
	return r;
}