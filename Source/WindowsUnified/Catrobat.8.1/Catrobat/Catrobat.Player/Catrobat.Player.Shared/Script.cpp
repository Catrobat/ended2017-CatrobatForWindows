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
#include "IChangeGhostEffectByBrick.h"
#include "ChangeGhostEffectByBrick.h"
#include "IChangeSizeByBrick.h"
#include "ChangeSizeByBrick.h"
#include "IChangeVariableBrick.h"
#include "ChangeVariableBrick.h"
#include "IChangeXByBrick.h"
#include "ChangeXByBrick.h"
#include "IChangeYByBrick.h"
#include "ChangeYByBrick.h"
#include "ICostumeBrick.h"
#include "CostumeBrick.h"
#include "IForeverBrick.h"
#include "ForeverBrick.h"
#include "IGlideBrick.h"
#include "GlideToBrick.h"
#include "IHideBrick.h"
#include "HideBrick.h"
#include "IIfBrick.h"
#include "IfBrick.h"
#include "IMoveNStepsBrick.h"
#include "MoveNStepsBrick.h"
#include "INextLookBrick.h"
#include "NextLookBrick.h"
#include "IPlaceAtBrick.h"
#include "PlaceAtBrick.h"
#include "IPlaySoundBrick.h"
#include "PlaySoundBrick.h"
#include "IPointToBrick.h"
#include "PointToBrick.h"
#include "IRepeatBrick.h"
#include "RepeatBrick.h"
#include "ISetGhostEffectBrick.h"
#include "SetGhostEffectBrick.h"
#include "ISetVariableBrick.h"
#include "SetVariableBrick.h"
#include "ISetXBrick.h"
#include "SetXBrick.h"
#include "ISetYBrick.h"
#include "SetYBrick.h"
#include "IShowBrick.h"
#include "ShowBrick.h"
#include "ITurnLeftBrick.h"
#include "TurnLeftBrick.h"

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

		auto broadcastBrick = dynamic_cast<Catrobat_Player::NativeComponent::IBroadcastBrick^>(brick);
		if (broadcastBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<BroadcastBrick>(broadcastBrick, this)));
			continue;
		}

		auto changeGhostEffectByBrick = dynamic_cast<Catrobat_Player::NativeComponent::IChangeGhostEffectByBrick^>(brick);
		if (changeGhostEffectByBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<ChangeGhostEffectByBrick>(changeGhostEffectByBrick, this)));
			continue;
		}

		auto changeSizeByBrick = dynamic_cast<Catrobat_Player::NativeComponent::IChangeSizeByBrick^>(brick);
		if (changeSizeByBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<ChangeSizeByBrick>(changeSizeByBrick, this)));
			continue;
		}

		auto changeVariableBrick = dynamic_cast<Catrobat_Player::NativeComponent::IChangeVariableBrick^>(brick);
		if (changeVariableBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<ChangeVariableBrick>(changeVariableBrick, this)));
			continue;
		}

		auto changeXByBrick = dynamic_cast<Catrobat_Player::NativeComponent::IChangeXByBrick^>(brick);
		if (changeXByBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<ChangeXByBrick>(changeXByBrick, this)));
			continue;
		}

		auto changeYByBrick = dynamic_cast<Catrobat_Player::NativeComponent::IChangeYByBrick^>(brick);
		if (changeYByBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<ChangeYByBrick>(changeYByBrick, this)));
			continue;
		}

		auto costumeBrick = dynamic_cast<Catrobat_Player::NativeComponent::ICostumeBrick^>(brick);
		if (costumeBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<CostumeBrick>(costumeBrick, this)));
			continue;
		}

		auto foreverBrick = dynamic_cast<Catrobat_Player::NativeComponent::IForeverBrick^>(brick);
		if (foreverBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<ForeverBrick>(foreverBrick, this)));
			continue;
		}

		auto glideToBrick = dynamic_cast<Catrobat_Player::NativeComponent::IGLideToBrick^>(brick);
		if (glideToBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<GlideToBrick>(glideToBrick, this)));
			continue;
		}

		auto hideBrick = dynamic_cast<Catrobat_Player::NativeComponent::IHideBrick^>(brick);
		if (hideBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<HideBrick>(this)));
			continue;
		}

		auto ifBrick = dynamic_cast<Catrobat_Player::NativeComponent::IIfBrick^>(brick);
		if (ifBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<IfBrick>(ifBrick, this)));
			continue;
		}

		auto moveNStepsBrick = dynamic_cast<Catrobat_Player::NativeComponent::IMoveNStepsBrick^>(brick);
		if (moveNStepsBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<MoveNStepsBrick>(moveNStepsBrick, this)));
			continue;
		}

		auto nextLookBrick = dynamic_cast<Catrobat_Player::NativeComponent::INextLookBrick^>(brick);
		if (nextLookBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<NextLookBrick>(this)));
			continue;
		}

		auto placeAtBrick = dynamic_cast<Catrobat_Player::NativeComponent::IPlaceAtBrick^>(brick);
		if (placeAtBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<PlaceAtBrick>(placeAtBrick, this)));
			continue;
		}

		auto playSoundBrick = dynamic_cast<Catrobat_Player::NativeComponent::IPlaySoundBrick^>(brick);
		if (playSoundBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<PlaySoundBrick>(playSoundBrick, this)));
			continue;
		}

		auto pointToBrick = dynamic_cast<Catrobat_Player::NativeComponent::IPointToBrick^>(brick);
		if (pointToBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<PointToBrick>(pointToBrick, this)));
			continue;
		}

		auto repeatBrick = dynamic_cast<Catrobat_Player::NativeComponent::IRepeatBrick^>(brick);
		if (repeatBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<RepeatBrick>(repeatBrick, this)));
			continue;
		}

		auto setGhostEffectBrick = dynamic_cast<Catrobat_Player::NativeComponent::ISetGhostEffectBrick^>(brick);
		if (setGhostEffectBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<SetGhostEffectBrick>(setGhostEffectBrick, this)));
			continue;
		}

		auto setVariableBrick = dynamic_cast<Catrobat_Player::NativeComponent::ISetVariableBrick^>(brick);
		if (setVariableBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<SetVariableBrick>(setVariableBrick, this)));
			continue;
		}

		auto setXBrick = dynamic_cast<Catrobat_Player::NativeComponent::ISetXBrick^>(brick);
		if (setXBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<SetXBrick>(setXBrick, this)));
			continue;
		}

		auto setYBrick = dynamic_cast<Catrobat_Player::NativeComponent::ISetYBrick^>(brick);
		if (setYBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<SetYBrick>(setYBrick, this)));
			continue;
		}

		auto showBrick = dynamic_cast<Catrobat_Player::NativeComponent::IShowBrick^>(brick);
		if (showBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<ShowBrick>(this)));
			continue;
		}

		auto turnLeftBrick = dynamic_cast<Catrobat_Player::NativeComponent::ITurnLeftBrick^>(brick);
		if (turnLeftBrick)
		{
			m_bricks.push_back(std::unique_ptr<Brick>(make_unique<TurnLeftBrick>(turnLeftBrick, this)));
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