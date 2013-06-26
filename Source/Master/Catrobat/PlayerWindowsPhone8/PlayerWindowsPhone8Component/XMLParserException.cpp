#include "pch.h"
#include "XMLParserException.h"

using namespace std;

XMLParserException::XMLParserException(string errorMessage, SeverityLevel::Severity severity)
    : m_errorMessage(errorMessage), m_severity(severity)
{
}

string XMLParserException::ErrorMessage()
{
    return m_errorMessage;
}

SeverityLevel::Severity XMLParserException::Level()
{
    return m_severity;
}