#pragma once

#define EPSILON 0.001f

class TestHelper
{
public:
	static bool isEqual(float x, float y);
    static bool isEqual(double x, double y);

    static std::string ConvertPlatformStringToString(Platform::String^ input);
    static Platform::String^ ConvertStringToPlatformString(std::string input);
};

