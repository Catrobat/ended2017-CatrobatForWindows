#pragma once

#include "XMLParserException.h"

#include <string>

class XMLParserSevereException : public XMLParserException
{
public:
    XMLParserSevereException(std::string errorMessage);
    std::string GetName();
};