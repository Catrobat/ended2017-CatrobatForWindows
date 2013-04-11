#include "pch.h"
#include "Brick.h"
#include "Script.h"
#include "Sprite.h"
#include "CostumeBrick.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;

Brick::Brick(TypeOfBrick brickType, string spriteReference, Script *parent) :
	m_brickType(brickType), m_spriteReference(spriteReference), m_parent(parent)
{
}

Brick::TypeOfBrick Brick::BrickType()
{
	return m_brickType;
}

void Brick::Execute()
{
	auto WorkItem = ref new WorkItemHandler(
		[this](IAsyncAction^ workItem)
	{
		while (true){
		//if (workItem->Status == Windows::Foundation::AsyncStatus::Canceled)
		// code to break a possible loop
		switch (m_brickType)
		{
		case Brick::CostumeBrick:
			{
				class::CostumeBrick *brick = (class::CostumeBrick *) this;
				m_parent->Parent()->SetLookData(brick->Index());
			}
			break;
		case Brick::WaitBrick:
			break;
		case Brick::PlaceAtBrick:
			break;
		case Brick::SetGhostEffectBrick:
			break;
		case Brick::PlaySoundBrick:
			break;
		default:
			break;
		}}
	});

	IAsyncAction ^ThreadPoolWorkItem = ThreadPool::RunAsync(WorkItem);

	Platform::String ^EventName = "ExampleEvent";
	HANDLE ExampleEvent = CreateEventEx(NULL, TEXT("ExampleEvent"), CREATE_EVENT_MANUAL_RESET, EVENT_ALL_ACCESS );

	Core::SignalNotifier ^NamedEventNotifier = Core::SignalNotifier::AttachToEvent(EventName,
		ref new Core::SignalHandler([this] (Core::SignalNotifier ^signalNotifier, bool timedOut)
	{
		// Multithreaded Event Handling happens here!!!
	}));

	NamedEventNotifier->Enable();
	SetEvent(ExampleEvent);
}

Script *Brick::Parent()
{
	return m_parent;
}