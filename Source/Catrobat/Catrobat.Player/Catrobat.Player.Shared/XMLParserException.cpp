#include "pch.h"
#include "XMLParserException.h"
#include "Helper.h"

XMLParserException::XMLParserException(std::string errorMessage)
    :BaseException(errorMessage)
{

}

XMLParserException::XMLParserException(void* exceptionThrownIn, std::string errorMessage)
    :BaseException(errorMessage)
{

}

std::string XMLParserException::GetName()
{
    return Helper::RetrieveClassName(typeid (XMLParserException).name());
}
