#include "pch.h"
#include "FormulaTreeException.h"
#include "Helper.h"

FormulaTreeException::FormulaTreeException(std::string errorMessage)
    :BaseException(errorMessage)
{

}

FormulaTreeException::FormulaTreeException(void* exceptionThrownIn, std::string errorMessage)
    :BaseException(errorMessage)
{

}

std::string FormulaTreeException::GetName()
{
    return Helper::RetrieveClassName(typeid (FormulaTreeException).name());
}
