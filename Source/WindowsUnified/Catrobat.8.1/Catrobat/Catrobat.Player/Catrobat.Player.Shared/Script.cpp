#include "pch.h"
#include "Script.h"
#include "Brick.h"
#include "ITurnRightBrick.h"
#include "TurnRightBrick.h"
#include "ISetSizeToBrick.h"
#include "SetSizeToBrick.h"
#include "IWaitBrick.h"
#include "WaitBrick.h"
#include "IBroadcastBrick.h"
#include "BroadcastBrick.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>
#include <ppltasks.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;
using namespace std;
using namespace ProjectStructure;

Script::Script(TypeOfScript scriptType, Object* parent, Catrobat_Player::NativeComponent::IScript^ script) :
	m_scriptType(scriptType), m_parent(parent)
{
	for each (Catrobat_Player::NativeComponent::IBrick^ brick in script->Bricks)
	{
		auto turnRightBrick = dynamic_cast<Catrobat_Player::NativeComponent::ITurnRightBrick^>(brick);
		if (turnRightBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<TurnRightBrick>(turnRightBrick, this)));
			continue;
		}

		auto setSizeToBrick = dynamic_cast<Catrobat_Player::NativeComponent::ISetSizeToBrick^>(brick);
		if (setSizeToBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<SetSizeToBrick>(setSizeToBrick, this)));
			continue;
		}

		auto waitBrick = dynamic_cast<Catrobat_Player::NativeComponent::IWaitBrick^>(brick);
		if (waitBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<WaitBrick>(waitBrick, this)));
			continue;
		}
	}
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

Object* Script::GetParent()
{
	return m_parent;
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