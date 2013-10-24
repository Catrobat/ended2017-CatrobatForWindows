#pragma once

#include "string"
#include "BaseException.h"

class XMLParserFatalException : public BaseException
{
public:
    XMLParserFatalException(std::string errorMessage);
    XMLParserFatalException(void* exceptionThrownIn, std::string errorMessage);
    std::string GetName();
};

