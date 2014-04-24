#include "pch.h"
#include <string>
#include "Helper.h"
#include <algorithm>
#include <limits.h>

std::string Helper::ConvertPlatformStringToString(Platform::String^ input)
{
    std::wstring foo(input->Begin());
    std::string res(foo.begin(), foo.end());
	return res;
}

Platform::String^ Helper::ConvertStringToPlatformString(std::string input)
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
