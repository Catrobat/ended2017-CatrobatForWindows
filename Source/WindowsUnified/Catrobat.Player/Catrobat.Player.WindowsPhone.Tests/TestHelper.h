#pragma once

#define EPSILON 0.001f

class TestHelper
{
public:
    static std::string ConvertPlatformStringToString(Platform::String^ input);
    static Platform::String^ ConvertStringToPlatformString(std::string input);
};

