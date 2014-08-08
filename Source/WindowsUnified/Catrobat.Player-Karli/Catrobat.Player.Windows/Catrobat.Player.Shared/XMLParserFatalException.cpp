#include "pch.h"
#include "XMLParserFatalException.h"
#include "Helper.h"

XMLParserFatalException::XMLParserFatalException(std::string errorMessage)
    :BaseException(errorMessage)
{

}

XMLParserFatalException::XMLParserFatalException(void* exceptionThrownIn, std::string errorMessage)
    :BaseException(errorMessage)
{

}

std::string XMLParserFatalException::GetName()
{
    return Helper::RetrieveClassName(typeid (XMLParserFatalException).name());
}

