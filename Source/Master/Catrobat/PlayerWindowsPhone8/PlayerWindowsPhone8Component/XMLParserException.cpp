#include "pch.h"
#include "XMLParserException.h"

using namespace std;

XMLParserException::XMLParserException(string errorMessage)
    : BaseException(errorMessage, ExceptionTypes::ExceptionType::XMLParserException)
{
}
