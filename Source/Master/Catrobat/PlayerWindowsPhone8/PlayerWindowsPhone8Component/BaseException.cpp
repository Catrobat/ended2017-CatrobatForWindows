#include "pch.h"
#include "BaseException.h"


BaseException::BaseException(std::string errorMessage)
    : m_errorMessage(errorMessage)
{
}
