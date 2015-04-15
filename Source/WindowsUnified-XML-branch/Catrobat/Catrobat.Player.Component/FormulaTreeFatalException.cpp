#include "pch.h"
#include "FormulaTreeFatalException.h"
#include "Helper.h"

FormulaTreeFatalException::FormulaTreeFatalException(std::string errorMessage)
    :BaseException(errorMessage)
{

}

FormulaTreeFatalException::FormulaTreeFatalException(void* exceptionThrownIn, std::string errorMessage)
    :BaseException(errorMessage)
{

}

std::string FormulaTreeFatalException::GetName()
{
    return Helper::RetrieveClassName(typeid (FormulaTreeFatalException).name());
}
