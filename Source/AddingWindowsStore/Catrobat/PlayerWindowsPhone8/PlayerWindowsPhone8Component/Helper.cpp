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