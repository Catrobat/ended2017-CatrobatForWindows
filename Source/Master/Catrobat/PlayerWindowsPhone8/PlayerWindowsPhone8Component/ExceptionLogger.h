#pragma once

#include "BaseException.h"

#define LOGFILE "logfile.txt"
#define INFORMATION 0
#define WARNING 1
#define CRITICALWARNING 2

class ExceptionLogger
{
public:
    static ExceptionLogger *Instance();
    void Log(BaseException *exception);
    void Log(int severity, std::string warning);

private:
    static ExceptionLogger *__instance;
    ExceptionLogger();
    Platform::String^ CalculateDate(unsigned int input);
};

