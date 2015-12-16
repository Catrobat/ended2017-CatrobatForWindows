#pragma once
#include <string>

class Helper
{
public:
    static std::string StdString(Platform::String^ input);
    static Platform::String^ PlatformString(std::string input);
    static std::string RetrieveClassName(std::string input);
    static bool ConvertStringToDouble(std::string s, double* d);
};

