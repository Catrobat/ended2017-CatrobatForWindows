#include "pch.h"
#include "XMLParserSevereException.h"

using namespace std;

XMLParserSevereException::XMLParserSevereException(string errorMessage)
    : XMLParserException (errorMessage)
{
}
