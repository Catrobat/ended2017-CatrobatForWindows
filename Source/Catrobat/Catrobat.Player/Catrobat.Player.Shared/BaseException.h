#pragma once

#include <string>

class BaseException
{
public:
    BaseException(std::string errorMessage);
    std::string GetErrorMessage();
    virtual std::string GetName() = 0;
    
private:
    std::string m_errorMessage;
};

