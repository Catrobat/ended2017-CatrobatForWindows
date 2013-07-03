#include "pch.h"
#include "ExceptionLogger.h"
#include "Helper.h"

#include <ppltasks.h> 
#include <vector>
#include <array>
#include <collection.h>

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
    task<StorageFile^> getFileTask(ApplicationData::Current->LocalFolder->CreateFileAsync(Helper::ConvertStringToPlatformString(LOGFILE), CreationCollisionOption::OpenIfExists));

    auto writer = std::make_shared<Streams::DataWriter^>(nullptr);

    getFileTask.then([](StorageFile^ file)
    {
        return file->OpenAsync(FileAccessMode::ReadWrite);
    }).then([this, exception, writer](Streams::IRandomAccessStream^ stream)
    {
        Streams::DataWriter^ state = ref new Streams::DataWriter(stream->GetOutputStreamAt(stream->Size));
        *writer = state;
        state->WriteString(Helper::ConvertStringToPlatformString(exception->GetErrorMessage()));

        return state->StoreAsync();
    }).then([writer](uint32 count)
    {
        return (*writer)->FlushAsync();
    }).then([this, writer](bool flushed)
    {
        delete (*writer);
    });
}