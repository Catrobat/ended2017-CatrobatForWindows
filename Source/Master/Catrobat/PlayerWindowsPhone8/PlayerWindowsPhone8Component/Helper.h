#pragma once
#include <string>

using namespace std;
using namespace Platform;

class Helper
{
public:
    static string ConvertPlatformStringToString(String^ input);
    static String^ ConvertStringToPlatformString(string input);
    static string RetrieveClassName(string input);
};

