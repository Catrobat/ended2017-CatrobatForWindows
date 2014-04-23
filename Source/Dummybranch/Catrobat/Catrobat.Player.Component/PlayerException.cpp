#include "pch.h"
#include "PlayerException.h"
#include "Helper.h"

using namespace std;

PlayerException::PlayerException(string errorMessage)
    : BaseException(errorMessage)
{
}

PlayerException::PlayerException(void* exceptionThrownIn, string errorMessage)
    : BaseException(errorMessage)
{
}

string PlayerException::GetName()
{
    return Helper::RetrieveClassName(typeid (PlayerException).name());
}
