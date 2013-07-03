#include "pch.h"
#include "BaseException.h"


BaseException::BaseException(std::string errorMessage, ExceptionTypes::ExceptionType exceptionType)
    : m_exceptionType(exceptionType), m_errorMessage(errorMessage)
{
}


BaseException::~BaseException()
{
}
