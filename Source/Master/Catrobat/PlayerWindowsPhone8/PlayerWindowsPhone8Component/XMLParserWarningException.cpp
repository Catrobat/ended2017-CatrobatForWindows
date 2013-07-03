#include "pch.h"
#include "XMLParserWarningException.h"

using namespace std;

XMLParserWarningException::XMLParserWarningException(string errorMessage)
    : XMLParserException (errorMessage)
{
}