#include "pch.h"
#include "BaseException.h"

using namespace std;

BaseException::BaseException(std::string errorMessage)
    : m_errorMessage(errorMessage)
{
}

string BaseException::GetErrorMessage()
{
    return m_errorMessage;
}
