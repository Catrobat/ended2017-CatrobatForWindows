#pragma once

#include "string"
#include "BaseException.h"

class FormulaTreeFatalException : public BaseException
{
public:
    FormulaTreeFatalException(std::string errorMessage);
    FormulaTreeFatalException(void* exceptionThrownIn, std::string errorMessage);
    std::string GetName();
};

