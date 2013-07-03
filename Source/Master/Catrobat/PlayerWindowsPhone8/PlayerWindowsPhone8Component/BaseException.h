#pragma once

#include <string>

class BaseException
{
public:
    BaseException(std::string errorMessage);
    std::string GetErrorMessage();
    
private:
    std::string m_errorMessage;
};

