#pragma once

#include <string>

class BaseException
{
public:
    BaseException(std::string errorMessage);

    
private:
    std::string m_errorMessage;
};

