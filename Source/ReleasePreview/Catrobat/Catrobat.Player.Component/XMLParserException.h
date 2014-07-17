#pragma once

#include "string"
#include "BaseException.h"

class XMLParserException : public BaseException
{
public:
    XMLParserException(std::string errorMessage);
    XMLParserException(void* exceptionThrownIn, std::string errorMessage);
    std::string GetName();
};


