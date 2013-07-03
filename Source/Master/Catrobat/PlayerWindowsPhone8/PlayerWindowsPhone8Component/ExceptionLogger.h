#pragma once

class ExceptionLogger
{
public:
    static ExceptionLogger *Instance();

private:
    static ExceptionLogger *__instance;
    ExceptionLogger();
};

