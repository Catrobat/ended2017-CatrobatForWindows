#include "pch.h"
#include "Script.h"
#include "Brick.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>
#include <ppltasks.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;
using namespace std;
using namespace ProjectStructure;

Script::Script(TypeOfScript scriptType, Object* parent) :
	m_scriptType(scriptType), m_parent(parent)
{
}

Script::~Script()
{
}

void Script::AddBrick(unique_ptr<Brick> brick)
{
	m_bricks.push_back(move(brick));
}

void Script::AddSpriteReference(std::string spriteReference)
{
	m_spriteReference = spriteReference;
}

Script::TypeOfScript Script::GetType()
{
	return m_scriptType;
}

void Script::Execute()
{
	auto workItem = ref new WorkItemHandler(
		[this](IAsyncAction^ workItem)
	{
		for each (auto &brick in m_bricks)
		{
			brick->Execute();
		}
		Concurrency::wait(10);
	});

	m_threadPoolWorkItem = ThreadPool::RunAsync(workItem);
}

shared_ptr<Object> Script::GetParent()
{
	return nullptr;
}

bool Script::IsRunning()
{
	if (m_threadPoolWorkItem == nullptr || 
		m_threadPoolWorkItem->Status != Windows::Foundation::AsyncStatus::Started)
	{
		return false;
	}
	return true;
}