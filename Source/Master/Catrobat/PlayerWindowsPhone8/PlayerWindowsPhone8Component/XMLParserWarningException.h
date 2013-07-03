#pragma once

#include "XMLParserException.h"

class XMLParserWarningException : public XMLParserException
{
public:
    XMLParserWarningException(std::string errorMessage);
};

