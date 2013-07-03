#pragma once

#include "BaseException.h"

#define LOGFILE "logfile.txt"

class ExceptionLogger
{
public:
    static ExceptionLogger *Instance();
    void LogException(BaseException *exception);

private:
    static ExceptionLogger *__instance;
    ExceptionLogger();
    Platform::String^ CalculateDate(unsigned int input);
};

