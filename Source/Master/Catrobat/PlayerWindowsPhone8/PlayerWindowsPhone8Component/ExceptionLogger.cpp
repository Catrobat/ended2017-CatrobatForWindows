#include "pch.h"
#include "ExceptionLogger.h"

ExceptionLogger *ExceptionLogger::__instance = NULL;

ExceptionLogger *ExceptionLogger::Instance()
{
    if (!__instance)
        __instance = new ExceptionLogger();
    return __instance;
}

ExceptionLogger::ExceptionLogger()
{
}
