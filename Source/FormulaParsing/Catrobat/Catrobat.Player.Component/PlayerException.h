#pragma once

#include "string"
#include "BaseException.h"

class PlayerException : public BaseException
{
public:
    PlayerException(std::string errorMessage);
    PlayerException(void* exceptionThrownIn, std::string errorMessage);
    std::string GetName();
};
