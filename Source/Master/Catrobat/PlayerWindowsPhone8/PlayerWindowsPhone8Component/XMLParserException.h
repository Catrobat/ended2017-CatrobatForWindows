#pragma once

#include "string"
namespace SeverityLevel
{
    public enum class Severity
    {
        SEVERE, 
        WARNING
    };
};

class XMLParserException
{
public:
    XMLParserException(std::string errorMessage, SeverityLevel::Severity severity);
    XMLParserException();

    std::string ErrorMessage(); 
    SeverityLevel::Severity Level();

private:
    std::string m_errorMessage;
    SeverityLevel::Severity m_severity;
};

