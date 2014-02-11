#include "pch.h"
#include "ScriptHandler.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;


ScriptHandler::ScriptHandler()
{
	auto WorkItem = ref new WorkItemHandler(
		[this](IAsyncAction^ workItem)
	{
		int x = 0;
		while(true)
		{
            if (workItem->Status == Windows::Foundation::AsyncStatus::Canceled)
            {
                break;
            }

			x += 1;
		}
	});

	IAsyncAction ^ThreadPoolWorkItem = ThreadPool::RunAsync(WorkItem);
}