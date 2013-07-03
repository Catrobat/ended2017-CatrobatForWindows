#pragma once

#include <string>

namespace ExceptionTypes
{
    public enum class ExceptionType 
    {
        XMLParserException
    };
};

class BaseException
{
public:
    BaseException(std::string errorMessage, ExceptionTypes::ExceptionType exceptionType);
    ~BaseException();

    
private:
    ExceptionTypes::ExceptionType m_exceptionType;
    std::string m_errorMessage;
};

