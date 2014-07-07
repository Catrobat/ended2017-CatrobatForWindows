#pragma once

#include "string"
#include "BaseException.h"

class FormulaTreeException : public BaseException
{
public:
    FormulaTreeException(std::string errorMessage);
    FormulaTreeException(void* exceptionThrownIn, std::string errorMessage);
    std::string GetName();
};

