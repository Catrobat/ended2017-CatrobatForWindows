#include "pch.h"
#include "XMLParserSevereException.h"
#include "Helper.h"

using namespace std;

XMLParserSevereException::XMLParserSevereException(string errorMessage)
    : XMLParserException (errorMessage)
{
}

string XMLParserSevereException::GetName()
{

    return Helper::RetrieveClassName(typeid (XMLParserSevereException).name());
}
