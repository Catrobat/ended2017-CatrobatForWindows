#include "pch.h"
#include "Helper.h"

string Helper::ConvertPlatformStringToString(Platform::String^ input)
{
	wstring foo(input->Begin());
	string res(foo.begin(), foo.end());
	return res;
}