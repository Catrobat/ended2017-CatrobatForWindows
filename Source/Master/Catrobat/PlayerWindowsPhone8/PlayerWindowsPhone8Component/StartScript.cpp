#include "pch.h"
#include "StartScript.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>
#include <ppltasks.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;

StartScript::StartScript(string spriteReference, Sprite *parent) :
	Script(TypeOfScript::StartScript, spriteReference, parent)
{
}

void StartScript::Execute()
{
	auto WorkItem = ref new WorkItemHandler(
		[this](IAsyncAction^ workItem)
	{
		list<Brick*>::iterator it;
		//while (true)
		{
			for(it = m_brickList->begin(); it != m_brickList->end(); it++)
			{
				(*it)->Execute();
			}
			Concurrency::wait(10);
		}
	});

	IAsyncAction^ ThreadPoolWorkItem = ThreadPool::RunAsync(WorkItem);
}