#pragma once
#include <string>

class Helper
{
public:
    static std::string ConvertPlatformStringToString(Platform::String^ input);
    static Platform::String^ ConvertStringToPlatformString(std::string input);
    static std::string RetrieveClassName(std::string input);
    static bool ConvertStringToDouble(std::string s, double* d);
};

