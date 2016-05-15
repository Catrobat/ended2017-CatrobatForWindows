#include "pch.h"
#include "OutOfBoundsException.h"
#include "Helper.h"

using namespace std;


OutOfBoundsException::OutOfBoundsException()
    : BaseException("Input out of bounds.")
{
}

string OutOfBoundsException::GetName()
{
    return Helper::RetrieveClassName(typeid (OutOfBoundsException).name());
}