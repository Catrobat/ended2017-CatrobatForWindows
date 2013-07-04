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

ExceptionLogger *ExceptionLogger::Instance()
{
    if (!__instance)
        __instance = new ExceptionLogger();
    return __instance;
}

ExceptionLogger::ExceptionLogger()
{
}

void ExceptionLogger::LogException(BaseException *exception)
{
    task<StorageFile^> getFileTask(ApplicationData::Current->LocalFolder->CreateFileAsync(
        Helper::ConvertStringToPlatformString(LOGFILE), CreationCollisionOption::OpenIfExists));
    auto writer = std::make_shared<Streams::DataWriter^>(nullptr);
    getFileTask.then([](StorageFile^ file)
    {
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
    });
}

String ^ExceptionLogger::CalculateDate(unsigned int input)
{
    if (input < 10)
        return L"0" + input.ToString();
    return input.ToString();
}