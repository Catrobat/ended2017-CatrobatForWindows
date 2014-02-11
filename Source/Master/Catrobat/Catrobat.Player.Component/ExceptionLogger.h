#pragma once

#include "BaseException.h"
#include <windows.h>

#define LOGFILE "logfile.txt"
#define INFORMATION 0
#define WARNING 1
#define CRITICALWARNING 2

class CritSection : public CRITICAL_SECTION
{
public:
    CritSection() 
    {
        InitializeCriticalSectionEx(this, 0, CRITICAL_SECTION_NO_DEBUG_INFO);
    }

    ~CritSection() 
    {
        DeleteCriticalSection(this);
    }

private:
    CritSection(CritSection const&);
    CritSection& operator=(CritSection const&);
};


class ExceptionLogger
{
public:
    static ExceptionLogger *Instance();
    void Log(BaseException *exception);
	void Log(Platform::Exception^ exception);
    void Log(int severity, std::string warning);

private:
    static CritSection __crit_section;
    static ExceptionLogger *__instance;

    ExceptionLogger();
    ~ExceptionLogger();
    ExceptionLogger (ExceptionLogger const&);
    ExceptionLogger& operator=(ExceptionLogger const&);

    Platform::String^ CalculateDate(unsigned int input);
};

