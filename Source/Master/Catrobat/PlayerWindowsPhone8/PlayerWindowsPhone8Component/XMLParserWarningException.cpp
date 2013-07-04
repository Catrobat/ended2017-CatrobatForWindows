#include "pch.h"
#include "XMLParserWarningException.h"
#include "Helper.h"

using namespace std;

XMLParserWarningException::XMLParserWarningException(string errorMessage)
    : XMLParserException (errorMessage)
{
}

string XMLParserWarningException::GetName()
{
    return Helper::RetrieveClassName(typeid (XMLParserWarningException).name());
}