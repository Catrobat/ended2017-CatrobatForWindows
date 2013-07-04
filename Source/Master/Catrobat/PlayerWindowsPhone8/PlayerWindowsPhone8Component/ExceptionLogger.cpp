#include "pch.h"
#include "ExceptionLogger.h"
#include "Helper.h"

#include <ppltasks.h> 
#include <vector>
#include <array>
#include <collection.h>
#include <time.h>

using namespace std;
using namespace concurrency;
using namespace Platform;
using namespace Windows::Foundation::Collections;
using namespace Windows::Storage;
using namespace Platform::Collections;


ExceptionLogger *ExceptionLogger::__instance = NULL;
CritSection ExceptionLogger::__crit_section;

ExceptionLogger *ExceptionLogger::Instance()
{
    EnterCriticalSection(&__crit_section);
    if (!__instance)
        __instance = new ExceptionLogger();
    LeaveCriticalSection(&__crit_section);
    return __instance;
}

ExceptionLogger::ExceptionLogger()
{
}

void ExceptionLogger::Log(BaseException *exception)
{
    task<StorageFile^> getFileTask(ApplicationData::Current->LocalFolder->CreateFileAsync(
        Helper::ConvertStringToPlatformString(LOGFILE), CreationCollisionOption::OpenIfExists));

    auto writer = std::make_shared<Streams::DataWriter^>(nullptr);
    getFileTask.then([](StorageFile^ file)
    {
        EnterCriticalSection(&__crit_section);
        return file->OpenAsync(FileAccessMode::ReadWrite);
    }).then([this, exception, writer](Streams::IRandomAccessStream^ stream)
    {
        time_t now = time(0);
        struct tm timeStamp;
        localtime_s(&timeStamp, &now);
        Streams::DataWriter^ state = ref new Streams::DataWriter(stream->GetOutputStreamAt(stream->Size));
        *writer = state;
        state->WriteString(L"[" + (timeStamp.tm_year + 1900).ToString() + L"-");
        state->WriteString(CalculateDate(timeStamp.tm_mon + 1) + L"-");
        state->WriteString(CalculateDate(timeStamp.tm_mday) + L" ");
        state->WriteString(CalculateDate(timeStamp.tm_hour) + L":");
        state->WriteString(CalculateDate(timeStamp.tm_min) + L":");
        state->WriteString(CalculateDate(timeStamp.tm_sec) + L"] ");
        state->WriteString(Helper::ConvertStringToPlatformString(exception->GetName()) + L": ");
        state->WriteString(Helper::ConvertStringToPlatformString(exception->GetErrorMessage()) + L"\n");

        return state->StoreAsync();
    }).then([writer](uint32 count)
    {
        return (*writer)->FlushAsync();
    }).then([this, writer](bool flushed)
    {
        delete (*writer);
        LeaveCriticalSection(&__crit_section);
    });
}

void ExceptionLogger::Log(int severity, std::string warning)
{
    task<StorageFile^> getFileTask(ApplicationData::Current->LocalFolder->CreateFileAsync(
        Helper::ConvertStringToPlatformString(LOGFILE), CreationCollisionOption::OpenIfExists));
    auto writer = std::make_shared<Streams::DataWriter^>(nullptr);
    getFileTask.then([](StorageFile^ file)
    {
        EnterCriticalSection(&__crit_section);
        return file->OpenAsync(FileAccessMode::ReadWrite);
    }).then([this, severity, warning, writer](Streams::IRandomAccessStream^ stream)
    {
        time_t now = time(0);
        struct tm timeStamp;
        localtime_s(&timeStamp, &now);
        Streams::DataWriter^ state = ref new Streams::DataWriter(stream->GetOutputStreamAt(stream->Size));
        *writer = state;
        state->WriteString(L"[" + (timeStamp.tm_year + 1900).ToString() + L"-");
        state->WriteString(CalculateDate(timeStamp.tm_mon + 1) + L"-");
        state->WriteString(CalculateDate(timeStamp.tm_mday) + L" ");
        state->WriteString(CalculateDate(timeStamp.tm_hour) + L":");
        state->WriteString(CalculateDate(timeStamp.tm_min) + L":");
        state->WriteString(CalculateDate(timeStamp.tm_sec) + L"] ");
        
        switch (severity)
        {
        case INFORMATION:
            state->WriteString(L"INFORMATION: " + Helper::ConvertStringToPlatformString(warning));
            break;
        case WARNING:
            state->WriteString(L"WARNING: " + Helper::ConvertStringToPlatformString(warning));
            break;
        case CRITICALWARNING:
            state->WriteString(L"CRITICALWARNING: " + Helper::ConvertStringToPlatformString(warning));
            break;
        default:
            break;
        }

        return state->StoreAsync();
    }).then([writer](uint32 count)
    {
        return (*writer)->FlushAsync();
    }).then([this, writer](bool flushed)
    {
        delete (*writer);
        LeaveCriticalSection(&__crit_section);
    });
}

String ^ExceptionLogger::CalculateDate(unsigned int input)
{
    if (input < 10)
        return L"0" + input.ToString();
    return input.ToString();
}