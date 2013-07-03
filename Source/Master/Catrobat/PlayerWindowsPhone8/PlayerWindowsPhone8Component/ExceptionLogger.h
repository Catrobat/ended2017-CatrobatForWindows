#pragma once

#include "BaseException.h"

class ExceptionLogger
{
public:
    static ExceptionLogger *Instance();
    void LogException(BaseException *exception);

private:
    static ExceptionLogger *__instance;
    ExceptionLogger();
};

