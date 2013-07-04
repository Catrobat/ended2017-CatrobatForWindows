#pragma once

#include "XMLParserException.h"

#include <string>

class XMLParserWarningException : public XMLParserException
{
public:
    XMLParserWarningException(std::string errorMessage);
    std::string GetName();
};

